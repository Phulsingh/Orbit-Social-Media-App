using Orbit.Application.DTOs.Auth;
using MediatR;


namespace Orbit.Application.Features.Auth.Queries
{
    public class LoginQuery :IRequest<AuthResponseDto>
    {
        public string username { get; set; }
        public string Password { get; set; }
    }
}
