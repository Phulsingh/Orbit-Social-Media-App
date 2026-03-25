using Orbit.Application.DTOs.Auth;
using Orbit.Application.Features.Auth.Command;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Auth.Handler
{
    public class ForgotPasswordHandler
        : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IPasswordResetService _service;

        public ForgotPasswordHandler(IPasswordResetService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(
            ForgotPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.ForgotPasswordAsync(
                new ForgotPasswordDto { Email = request.Email });
        }
    }
}