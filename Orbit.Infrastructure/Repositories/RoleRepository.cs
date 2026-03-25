using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using Orbit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Orbit.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationRoles>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationRoles?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task AddRoleAsync(ApplicationRoles role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(ApplicationRoles role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if(role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

    }
}
