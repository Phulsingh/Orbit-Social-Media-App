

namespace Orbit.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(
            Stream fileStream, 
            string fileName, 
            string contentType, 
            string folderName
            );
    }
}
