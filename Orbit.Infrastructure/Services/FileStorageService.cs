using Microsoft.AspNetCore.Hosting;
using Orbit.Application.Interfaces;

namespace Orbit.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            string folderName)
        {
            // ✅ Null and empty check
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("File is empty.");

            // ✅ Size validation (5MB max)
            if (fileStream.Length > 5 * 1024 * 1024)
                throw new Exception("File size must be under 5MB.");

            // ✅ Extension validation
            var extension = Path.GetExtension(fileName).ToLower();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(extension))
                throw new Exception("Only .jpg, .jpeg and .png files are allowed.");

            // ✅ Safe path — works on all environments
            var uploadsPath = Path.Combine(
                _env.ContentRootPath, "wwwroot", "uploads", folderName);

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var newFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(uploadsPath, newFileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return $"/uploads/{folderName}/{newFileName}";
        }
    }
}