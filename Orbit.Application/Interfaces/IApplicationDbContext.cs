using Orbit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orbit.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        IQueryable<ApplicationUsers> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
