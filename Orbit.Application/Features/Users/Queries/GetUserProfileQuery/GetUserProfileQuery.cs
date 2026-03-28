using MediatR;
using Orbit.Application.DTOs.Users;

public class GetUserProfileQuery : IRequest<UserProfileDto>
{
    public int UserId { get; set; }
}