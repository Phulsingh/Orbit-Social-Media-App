using MediatR;


namespace Orbit.Application.Features.Auth.Command
{
    public class ResetPasswordCommand: IRequest<bool>
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
