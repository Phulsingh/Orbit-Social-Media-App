using Orbit.Domain.Entities;


namespace Orbit.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUsers user, List<string> roles);
    }
}
