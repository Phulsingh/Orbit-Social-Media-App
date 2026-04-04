using MediatR;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Features.Family.Queries;
using Orbit.Application.Interfaces;

namespace Orbit.Application.Features.Family.Handlers
{
    public class GetFamilyHandler
        : IRequestHandler<GetFamilyQuery, FamilyResponseDto>
    {
        private readonly IFamilyService _familyService;

        public GetFamilyHandler(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        public async Task<FamilyResponseDto> Handle(
            GetFamilyQuery request,
            CancellationToken cancellationToken)
        {
            return await _familyService
                .GetFamilyAsync(request.FamilyId, request.UserId);
        }
    }
}