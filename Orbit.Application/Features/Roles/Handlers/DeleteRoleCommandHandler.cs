using Orbit.Application.Interfaces;
using MediatR;

namespace Orbit.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler
    : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _repository;

    public DeleteRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetRoleByIdAsync(request.Id);

        if (role == null)
            return false;

        await _repository.DeleteRoleAsync(request.Id);

        return true;
    }
}