using Orbit.Application.DTOs.Auth;
using Orbit.Application.Features.Auth.Command;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponseDto> Handle(
           RegisterCommand request,
           CancellationToken cancellationToken)
        {
            // Map Command → DTO
            var dto = new RegisterRequestDto
            {
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                RoleIds = request.RoleIds
            };

            return await _authService.RegisterAsync(dto);
        }
    }
}
