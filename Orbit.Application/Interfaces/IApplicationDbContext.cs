using Orbit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orbit.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        // Existing
        DbSet<ApplicationRoles> Roles { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }

        // New
        DbSet<Family> Families { get; set; }
        DbSet<FamilyMember> FamilyMembers { get; set; }
        DbSet<InviteCode> InviteCodes { get; set; }

        IQueryable<ApplicationUsers> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
