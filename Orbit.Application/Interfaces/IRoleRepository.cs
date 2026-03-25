using Orbit.Domain.Entities;

namespace Orbit.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRoles>> GetAllRolesAsync();

        Task<ApplicationRoles?> GetRoleByIdAsync(int id);

        Task AddRoleAsync(ApplicationRoles role);

        Task UpdateRoleAsync(ApplicationRoles role);

        Task DeleteRoleAsync(int id);
    }
}
