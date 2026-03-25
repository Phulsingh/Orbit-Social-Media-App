
namespace Orbit.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        // User can send either Email or UserName
        public string username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
