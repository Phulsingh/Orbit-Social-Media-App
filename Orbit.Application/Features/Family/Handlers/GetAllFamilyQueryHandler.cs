using MediatR;
using Orbit.Application.Features.Family.Queries;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Interfaces;

namespace Orbit.Application.Features.Family.Handlers
{
    public class GetAllFamilyQueryHandler :IRequestHandler<GetAllFamilyQuery, List<FamilyResponseDto>>
    {
        private readonly IFamilyService _familyService;
        public GetAllFamilyQueryHandler(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        public async Task<List<FamilyResponseDto>> Handle(
           GetAllFamilyQuery request,
           CancellationToken cancellationToken)
        {
            // ✅ Just call service — no logic here
            return await _familyService.GetAllFamilyAsync();
        }

    }
}
