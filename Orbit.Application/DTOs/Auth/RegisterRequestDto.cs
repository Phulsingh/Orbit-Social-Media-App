
namespace Orbit.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
