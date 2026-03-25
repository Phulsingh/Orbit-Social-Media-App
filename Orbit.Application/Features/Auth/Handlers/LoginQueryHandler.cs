using Orbit.Application.DTOs.Auth;
using Orbit.Application.Features.Auth.Queries;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Auth.Handlers
{
    public class LoginQueryHandler: IRequestHandler<LoginQuery, AuthResponseDto>
        {
        private readonly IAuthService _authService;
        public LoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<AuthResponseDto> Handle(
           LoginQuery request,
           CancellationToken cancellationToken)
        {
            // Map Query → DTO
            var dto = new LoginRequestDto
            {
                username = request.username,
                Password = request.Password
            };
            return await _authService.LoginAsync(dto);
        }
    }
}
