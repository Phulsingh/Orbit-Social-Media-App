using MediatR;
using Orbit.Application.Features.Family.Commands;
using Orbit.Application.Interfaces;

public class ApproveMemberCommandHandler
    : IRequestHandler<ApproveMemberCommand, bool>
{
    private readonly IFamilyService _familyService;

    public ApproveMemberCommandHandler(IFamilyService familyService)
    {
        _familyService = familyService;
    }

    public async Task<bool> Handle(
        ApproveMemberCommand request,
        CancellationToken cancellationToken)
    {
        // ✅ Just call the service — no logic here
        return await _familyService.ApproveMemberAsync(
            request.FamilyId,
            request.MemberId,
            request.AdminUserId);
    }
}