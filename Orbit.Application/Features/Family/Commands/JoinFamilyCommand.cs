using MediatR;
using Orbit.Application.DTOs.Family;


namespace Orbit.Application.Features.Family.Commands
{
    public class JoinFamilyCommand: IRequest<FamilyResponseDto>
    {
        public int UserId { get; set; }
        public string InviteCode { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
    }
}
