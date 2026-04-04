using MediatR;
using Microsoft.EntityFrameworkCore;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using Orbit.Application.Features.Family.Commands;

namespace Orbit.Application.Features.Family.Handlers
{
    public class GenerateInviteCodeCommandHandler
        : IRequestHandler<GenerateInviteCodeCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public GenerateInviteCodeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(
            GenerateInviteCodeCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Check Family exists
            var familyExists = await _context.Families
                .AnyAsync(f => f.Id == request.FamilyId, cancellationToken);
            if (!familyExists)
                throw new Exception("Family not found");

            // 2. Verify user is Admin of this family
            var isAdmin = await _context.FamilyMembers
                .AnyAsync(m => m.FamilyId == request.FamilyId
                            && m.UserId == request.AdminUserId
                            && m.IsAdmin,
                        cancellationToken);
            if (!isAdmin)
                throw new Exception("Only admin can generate invite codes");

            // 3. Expire old unused codes
            var oldCodes = await _context.InviteCodes
                .Where(c => c.FamilyId == request.FamilyId && !c.IsUsed)
                .ToListAsync(cancellationToken);
            foreach (var old in oldCodes)
                old.IsUsed = true;

            // 4. Generate strong unique code
            var code = $"FN-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

            // 5. Save new InviteCode
            var inviteCode = new InviteCode
            {
                FamilyId = request.FamilyId,
                CreatedBy = request.AdminUserId,
                Code = code,
                IsUsed = false,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            _context.InviteCodes.Add(inviteCode);
            await _context.SaveChangesAsync(cancellationToken);

            return code;
        }
    }
}