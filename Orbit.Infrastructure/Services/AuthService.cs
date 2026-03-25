using Orbit.Application.DTOs.Auth;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using Orbit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Orbit.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context, IJwtService jwtService)
        {
             _context = context;
            _jwtService = jwtService;
        }

        // REGISTER
        // ─────────────────────────────────────────────────
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            // 1. Check duplicate Email
            if ( await _context.Users.AnyAsync( u => u.Email == dto.Email))
            {
                throw new Exception("Email already in use");

            }
            // 2. Check duplicate UserName
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
            {
                throw new Exception("Username already in use");
            }

            // 3. Create user with hashed password
            var user = new ApplicationUsers
            {
                Name = dto.Name,
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow 
            };

            _context.Users.Add(user);
             await _context.SaveChangesAsync();

            // 4. Assign roles by RoleIds
            var roleNames = new List<string>();

            if(dto.RoleIds != null && dto.RoleIds.Any())
            {
                var roleName = await _context.Roles
                     .Where(r => dto.RoleIds.Contains(r.Id) && !r.IsDeleted && r.IsActive && r.Name != null)
                     .ToListAsync();

                foreach(var role in roleName)
                {
                    _context.UserRoles.Add(new UserRoles
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });

                    if (!string.IsNullOrEmpty(role.Name))
                        roleNames.Add(role.Name);
                }
                await _context.SaveChangesAsync();
            }

            // 5. Generate token
            var expiry = DateTime.UtcNow.AddMinutes(60);
            var token = _jwtService.GenerateToken(user, roleNames);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiry,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roleNames
            };
        }

        // LOGIN
        // ─────────────────────────────────────────────────
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            // 1. Find user by UserName or Email — include roles
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    (u.UserName == dto.username ||
                     u.Email == dto.username)
                    && !u.IsDeleted);

            // 2. Check user exists and is active
            if (user == null || !user.IsActive)
                throw new Exception("Invalid username or password.");

            // 3. ✅ Verify BCrypt password .
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid username or password.");

            // 4. Get active role names
            var roleNames = user.UserRoles
                .Where(ur => ur.Role != null
                          && ur.Role.IsActive
                          && !ur.Role.IsDeleted)
                .Select(ur => ur.Role.Name)
                .ToList();

            // 5. Generate token with roles
            var expiry = DateTime.UtcNow.AddMinutes(60);
            var token = _jwtService.GenerateToken(user, roleNames);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiry,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roleNames
            };
        }

    }
}
