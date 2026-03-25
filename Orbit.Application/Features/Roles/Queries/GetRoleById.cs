
using Orbit.Application.DTOs;
using MediatR;

namespace Orbit.Application.Features.Roles.Queries
{
    public record GetRoleByIdQuery(int Id) : IRequest<RolesDTO>;
}
