using Microsoft.EntityFrameworkCore;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using Orbit.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Infrastructure.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly ApplicationDbContext _context;

        public FamilyService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────
        // CREATE FAMILY
        // ─────────────────────────────────────────────

        public async Task<FamilyResponseDto> CreateFamilyAsync(
             int userId, CreateFamilyDto dto)
        {
            // Check user exists
            var user = await _context.Users.FindAsync(userId)
                ?? throw new Exception("User not found.");

            // ✅ Check if user is already in a family
            var existingMembership = await _context.FamilyMembers
                .FirstOrDefaultAsync(m => m.UserId == userId);

            if (existingMembership != null)
            {
                // ✅ If already in family BUT is Admin → allow creating new family
                // ❌ If already in family AND is NOT Admin → block
                if (!existingMembership.IsAdmin)
                    throw new Exception(
                        "You already belong to a family. " +
                        "Only an admin can create a new family.");
            }

            var family = new Family
            {
                FamilyName = dto.FamilyName,
                FamilyCode = dto.FamilyCode,
                Description = dto.Description,
                CreatedBy = userId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Families.Add(family);
            await _context.SaveChangesAsync(CancellationToken.None);

            // Add creator as Admin member
            var member = new FamilyMember
            {
                FamilyId = family.Id,
                UserId = userId,
                IsAdmin = true,       // Creator is admin
                IsApproved = true,       // Auto approved
                JoinedAt = DateTime.UtcNow
            };

            _context.FamilyMembers.Add(member);
            await _context.SaveChangesAsync(CancellationToken.None);

            return MapToDto(family, 1);
        }

        // ─────────────────────────────────────────────
        // GET FAMILY
        // ─────────────────────────────────────────────

        public async Task<FamilyResponseDto> GetFamilyAsync(
            int familyId, int userId)
        {
            var family = await _context.Families
                .Include(f => f.FamilyMembers)
                .FirstOrDefaultAsync(f => f.Id == familyId && !f.IsDeleted)
                ?? throw new Exception("Family not found.");

            // Check user is member
            var isMember = family.FamilyMembers
                .Any(m => m.UserId == userId && m.IsApproved);

            if (!isMember)
                throw new Exception("You are not a member of this family.");

            return MapToDto(family, family.FamilyMembers.Count(m => m.IsApproved));
        }
        // ─────────────────────────────────────────────
        // GET MEMBERS
        // ─────────────────────────────────────────────
        public async Task<List<FamilyMemberDto>> GetMembersAsync(int familyId)
        {
            var members = await _context.FamilyMembers
                .Include(m => m.User)
                .Where(m => m.FamilyId == familyId && m.IsApproved)
                .ToListAsync();

            return members.Select(m => new FamilyMemberDto
            {
                UserId = m.UserId,
                Name = m.User.Name,
                UserName = m.User.UserName,
                ProfileUrl = m.User.ProfileUrl,
                Relation = m.Relation,
                Country = m.User.Country,
                IsAdmin = m.IsAdmin,
                IsApproved = m.IsApproved,
                JoinedAt = m.JoinedAt
            }).ToList();
        }

        // ─────────────────────────────────────────────
        // GENERATE INVITE CODE
        // ─────────────────────────────────────────────
        public async Task<string> GenerateInviteCodeAsync(
            int familyId, int adminUserId)
        {
            // Verify admin
            var isAdmin = await _context.FamilyMembers
                .AnyAsync(m => m.FamilyId == familyId
                            && m.UserId == adminUserId
                            && m.IsAdmin);

            if (!isAdmin)
                throw new Exception("Only admin can generate invite codes.");

            // Expire old unused codes
            var oldCodes = await _context.InviteCodes
                .Where(c => c.FamilyId == familyId && !c.IsUsed)
                .ToListAsync();

            foreach (var old in oldCodes)
                old.IsUsed = true;

            // Generate new code
            var code = $"FN-{new Random().Next(1000, 9999)}";

            var inviteCode = new InviteCode
            {
                FamilyId = familyId,
                CreatedBy = adminUserId,
                Code = code,
                IsUsed = false,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            _context.InviteCodes.Add(inviteCode);
            await _context.SaveChangesAsync(CancellationToken.None);

            return code;
        }

        // ─────────────────────────────────────────────
        // JOIN FAMILY
        // ─────────────────────────────────────────────
        public async Task<FamilyResponseDto> JoinFamilyAsync(
           int userId, JoinFamilyDto dto)
        {
            // Find valid invite code
            var invite = await _context.InviteCodes
                .Include(c => c.Family)
                .FirstOrDefaultAsync(c =>
                    c.Code == dto.InviteCode
                    && !c.IsUsed
                    && c.ExpiresAt > DateTime.UtcNow)
                ?? throw new Exception("Invalid or expired invite code.");

            // Check not already a member
            var alreadyMember = await _context.FamilyMembers
                .AnyAsync(m => m.FamilyId == invite.FamilyId
                            && m.UserId == userId);

            if (alreadyMember)
                throw new Exception("You are already a member of this family.");

            // Add as pending member
            var member = new FamilyMember
            {
                FamilyId = invite.FamilyId,
                UserId = userId,
                Relation = dto.Relation,
                IsAdmin = false,
                IsApproved = false,   // Pending admin approval
                JoinedAt = DateTime.UtcNow
            };

            _context.FamilyMembers.Add(member);

            // Mark invite code as used
            invite.IsUsed = true;
            invite.UsedBy = userId;

            await _context.SaveChangesAsync(CancellationToken.None);

            var totalMembers = await _context.FamilyMembers
                .CountAsync(m => m.FamilyId == invite.FamilyId && m.IsApproved);

            return MapToDto(invite.Family, totalMembers);
        }
        // ─────────────────────────────────────────────
        // APPROVE MEMBER
        // ─────────────────────────────────────────────
        public async Task<bool> ApproveMemberAsync(
            int familyId, int memberId, int adminUserId)
        {
            // Verify admin
            var isAdmin = await _context.FamilyMembers
                .AnyAsync(m => m.FamilyId == familyId
                            && m.UserId == adminUserId
                            && m.IsAdmin);

            if (!isAdmin)
                throw new Exception("Only admin can approve members.");

            var member = await _context.FamilyMembers
                .FirstOrDefaultAsync(m => m.Id == memberId
                                       && m.FamilyId == familyId)
                ?? throw new Exception("Member not found.");

            member.IsApproved = true;
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        // ─────────────────────────────────────────────
        // REMOVE MEMBER
        // ─────────────────────────────────────────────
        public async Task<bool> RemoveMemberAsync(
            int familyId, int memberId, int adminUserId)
        {
            var isAdmin = await _context.FamilyMembers
                .AnyAsync(m => m.FamilyId == familyId
                            && m.UserId == adminUserId
                            && m.IsAdmin);

            if (!isAdmin)
                throw new Exception("Only admin can remove members.");

            var member = await _context.FamilyMembers
                .FirstOrDefaultAsync(m => m.Id == memberId
                                       && m.FamilyId == familyId)
                ?? throw new Exception("Member not found.");

            _context.FamilyMembers.Remove(member);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        private static FamilyResponseDto MapToDto(Family family, int totalMembers)
        {
            return new FamilyResponseDto
            {
                Id = family.Id,
                FamilyName = family.FamilyName,
                FamilyCode = family.FamilyCode,
                Description = family.Description,
                PhotoUrl = family.PhotoUrl,
                TotalMembers = totalMembers,
                CreatedAt = family.CreatedAt
            };
        }
    }
}
