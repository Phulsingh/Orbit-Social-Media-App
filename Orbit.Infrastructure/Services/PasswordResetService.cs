using Orbit.Application.DTOs.Auth;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using Orbit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Orbit.Infrastructure.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public PasswordResetService(
            ApplicationDbContext context,
            IEmailService emailService,
            IConfiguration config)
        {
            _context = context;
            _emailService = emailService;
            _config = config;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            // 1. Check email exists
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email
                                       && !u.IsDeleted
                                       && u.IsActive);

            // Always return true — don't reveal if email exists
            if (user == null) return true;

            // 2. Delete old unused tokens for this user
            var oldTokens = await _context.PasswordResetTokens
                .Where(t => t.UserId == user.Id && !t.IsUsed)
                .ToListAsync();

            _context.PasswordResetTokens.RemoveRange(oldTokens);

            // 3. Generate new token
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                               .Replace("/", "_")
                               .Replace("+", "-")
                               .TrimEnd('=');

            // 4. Save token to DB
            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30), // 30 min expiry
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.PasswordResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            // 5. Build reset link
            var frontendUrl = _config["FrontendUrl"];
            var resetLink = $"{frontendUrl}/reset-password?token={token}";

            // 6. Send email
            await _emailService.SendPasswordResetEmailAsync(
                user.Email, resetLink);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // 1. Check passwords match
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("Passwords do not match.");

            // 2. Find valid token
            var resetToken = await _context.PasswordResetTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t =>
                    t.Token == dto.Token &&
                    !t.IsUsed &&
                    t.ExpiresAt > DateTime.UtcNow); // not expired

            if (resetToken == null)
                throw new Exception("Invalid or expired reset link.");

            // 3. Update password with BCrypt hash
            resetToken.User.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // 4. Mark token as used
            resetToken.IsUsed = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}