using MediatR;
using Orbit.Application.DTOs;
using Orbit.Application.Interfaces;
using Orbit.Application.Features.Roles.Queries;

namespace Orbit.Application.Features.Roles.Handlers;

public class GetRolesQueryHandler
    : IRequestHandler<GetRolesQuery, IEnumerable<RolesDTO>>
{
    private readonly IRoleRepository _repository;

    public GetRolesQueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RolesDTO>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _repository.GetAllRolesAsync();

        if (roles == null)
            return new List<RolesDTO>();

        return roles.Select(r => new RolesDTO
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive
        });
    }
}