using Orbit.Application.DTOs;
using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler
    : IRequestHandler<UpdateRoleCommand, RolesDTO>
{
    private readonly IRoleRepository _repository;

    public UpdateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<RolesDTO> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetRoleByIdAsync(request.Id);

        if (role == null)
            return null;

        role.Name = request.Name;
        role.Description = request.Description;
        role.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateRoleAsync(role);

        return new RolesDTO
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }
}