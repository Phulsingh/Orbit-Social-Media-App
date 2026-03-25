using Orbit.Application.DTOs;
using MediatR;

namespace Orbit.Application.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(
    int Id,
    string Name,
    string Description
) : IRequest<RolesDTO>;