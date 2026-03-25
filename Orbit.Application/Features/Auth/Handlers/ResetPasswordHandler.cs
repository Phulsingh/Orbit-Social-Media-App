using Orbit.Application.DTOs.Auth;
using Orbit.Application.Features.Auth.Command;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Auth.Handler
{
    public class ResetPasswordHandler
        : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IPasswordResetService _service;

        public ResetPasswordHandler(IPasswordResetService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.ResetPasswordAsync(new ResetPasswordDto
            {
                Token = request.Token,
                NewPassword = request.NewPassword,
                ConfirmPassword = request.ConfirmPassword
            });
        }
    }
}