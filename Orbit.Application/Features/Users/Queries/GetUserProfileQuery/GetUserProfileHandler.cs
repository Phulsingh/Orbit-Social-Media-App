using MediatR;
using Microsoft.EntityFrameworkCore;
using Orbit.Application.DTOs.Users;
using Orbit.Application.Interfaces;

public class GetUserProfileHandler
    : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IApplicationDbContext _context;

    public GetUserProfileHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Id == request.UserId && !x.IsDeleted)
            .Select(x => new UserProfileDto
            {
                Id = x.Id,
                Name = x.Name,
                ProfileUrl = x.ProfileUrl,
                UserName = x.UserName,
                Email = x.Email,
                Phone = x.Phone,
                Country = x.Country,
                State = x.State,
                Bio = x.Bio,
                IsOnline = x.IsOnline,
                LastSeenAt = x.LastSeenAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new Exception("User not found");

        return user;
    }
}