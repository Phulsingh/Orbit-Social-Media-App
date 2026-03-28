using MediatR;
using Microsoft.EntityFrameworkCore;
using Orbit.Application.Interfaces;

namespace Orbit.Application.Features.Users.Commands.UploadProfileImage
{
    public class UploadProfileImageHandler
        : IRequestHandler<UploadProfileImageCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileService;

        public UploadProfileImageHandler(
            IApplicationDbContext context,
            IFileStorageService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<string> Handle(
            UploadProfileImageCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                   .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found.");

            var url = await _fileService.UploadFileAsync(
                request.FileStream,
                request.FileName,
                request.ContentType,
                "profile"
            );

            user.ProfileUrl = url;
            await _context.SaveChangesAsync(cancellationToken);

            return url;
        }
    }
}