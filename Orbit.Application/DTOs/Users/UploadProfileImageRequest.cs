using Microsoft.AspNetCore.Http;


namespace Orbit.Application.DTOs.Users
{
    public class UploadProfileImageRequest
    {
        public IFormFile File { get; set; } = null!;
    }
}
