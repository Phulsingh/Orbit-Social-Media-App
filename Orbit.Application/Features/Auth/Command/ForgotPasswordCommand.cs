using MediatR;


namespace Orbit.Application.Features.Auth.Command
{
    public class ForgotPasswordCommand: IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}
