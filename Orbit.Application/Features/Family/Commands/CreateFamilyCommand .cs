using MediatR;
using Orbit.Application.DTOs.Family;

namespace Orbit.Application.Features.Family.Commands
{
    public class CreateFamilyCommand : IRequest<FamilyResponseDto>
    {
        public int UserId { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public string FamilyCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
