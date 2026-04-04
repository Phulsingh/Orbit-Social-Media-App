using MediatR;

namespace Orbit.Application.Features.Family.Commands
{
    public class GenerateInviteCodeCommand : IRequest<string>
    {
        public int FamilyId { get; set; }
        public int AdminUserId { get; set; }
    }
}
