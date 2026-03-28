using MediatR;
using System.Text.Json.Serialization;

namespace Orbit.Application.Features.Users.Commands.UploadProfileImage
{

    public class UploadProfileImageCommand : IRequest<string>
    {
        public int UserId { get; set;}


        public Stream FileStream { get; set; } = Stream.Null;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
