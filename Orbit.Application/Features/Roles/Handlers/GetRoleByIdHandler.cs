using Orbit.Application.DTOs;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, RolesDTO>
{
    private readonly IRoleRepository _repository;

    public GetRoleByIdQueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<RolesDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetRoleByIdAsync(request.Id);

        if (role == null)
            return null;

        return new RolesDTO
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
        };
    }
}