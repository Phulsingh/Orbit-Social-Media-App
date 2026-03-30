using MediatR;
using Orbit.Application.DTOs.Family;
using Orbit.Application.Features.Family.Commands;
using Orbit.Application.Interfaces;


namespace Orbit.Application.Features.Family.Handlers
{
    public class CreateFamilyHandler :IRequestHandler<CreateFamilyCommand, FamilyResponseDto>
    {
        private readonly IFamilyService _familyService;

        public CreateFamilyHandler(IFamilyService familyService)
        {
            _familyService = familyService;
        }
        public async Task<FamilyResponseDto> Handle(
           CreateFamilyCommand request,
           CancellationToken cancellationToken)
        {
            var dto = new CreateFamilyDto
            {
                FamilyName = request.FamilyName,
                Description = request.Description,
                FamilyCode = request.FamilyCode
            };

            return await _familyService.CreateFamilyAsync(request.UserId, dto);
        }


     }
}
