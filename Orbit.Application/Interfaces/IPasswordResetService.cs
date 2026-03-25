using Orbit.Application.DTOs.Auth;


namespace Orbit.Application.Interfaces
{
    public interface IPasswordResetService
    {
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
