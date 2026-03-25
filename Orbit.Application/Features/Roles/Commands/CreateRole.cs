using MediatR;


namespace Orbit.Application.Features.Roles.Commands
{
    public record CreateRoleCommand(
     string Name,
     string Description,
     bool IsActive
    ) : IRequest<int>;
}
