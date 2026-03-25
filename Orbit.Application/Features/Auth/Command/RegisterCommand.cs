using Orbit.Application.DTOs.Auth;
using MediatR;


namespace Orbit.Application.Features.Auth.Command
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
