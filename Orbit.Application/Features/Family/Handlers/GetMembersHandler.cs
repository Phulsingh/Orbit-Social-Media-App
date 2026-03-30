using MediatR;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Features.Family.Queries;
using Orbit.Application.Interfaces;

namespace Orbit.Application.Features.Family.Handlers
{
    public class GetMembersHandler
        : IRequestHandler<GetMembersQuery, List<FamilyMemberDto>>
    {
        private readonly IFamilyService _familyService;

        public GetMembersHandler(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        public async Task<List<FamilyMemberDto>> Handle(
            GetMembersQuery request,
            CancellationToken cancellationToken)
        {
            return await _familyService.GetMembersAsync(request.FamilyId);
        }
    }
}