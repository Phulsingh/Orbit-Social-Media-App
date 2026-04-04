using MediatR;
using Orbit.Application.Features.Family.Commands;
using Orbit.Application.Interfaces;

public class RemoveMemberCommandHandler
    : IRequestHandler<RemoveMemberCommand, bool>
{
    private readonly IFamilyService _familyService;

    public RemoveMemberCommandHandler(IFamilyService familyService)
    {
        _familyService = familyService;
    }

    public async Task<bool> Handle(
        RemoveMemberCommand request,
        CancellationToken cancellationToken)
    {
        // ✅ Just call the service — no logic here
        return await _familyService.RemoveMemberAsync(
            request.FamilyId,
            request.MemberId,
            request.AdminUserId);
    }
}