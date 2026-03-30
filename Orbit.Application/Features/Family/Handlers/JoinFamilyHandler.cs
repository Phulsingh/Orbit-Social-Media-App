using MediatR;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Features.Family.Commands;
using Orbit.Application.Interfaces;

namespace Orbit.Application.Features.Family.Handlers
{
    public class JoinFamilyHandler
        : IRequestHandler<JoinFamilyCommand, FamilyResponseDto>
    {
        private readonly IFamilyService _familyService;

        public JoinFamilyHandler(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        public async Task<FamilyResponseDto> Handle(
            JoinFamilyCommand request,
            CancellationToken cancellationToken)
        {
            var dto = new JoinFamilyDto
            {
                InviteCode = request.InviteCode,
                Relation = request.Relation
            };

            return await _familyService.JoinFamilyAsync(request.UserId, dto);
        }
    }
}