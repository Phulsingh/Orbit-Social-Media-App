using MediatR;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;

namespace Orbit.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IRoleRepository _repository;

    public CreateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = new ApplicationRoles
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddRoleAsync(role);

        return role.Id;
    }
}