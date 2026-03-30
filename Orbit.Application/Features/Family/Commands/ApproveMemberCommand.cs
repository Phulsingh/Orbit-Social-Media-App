using MediatR;


namespace Orbit.Application.Features.Family.Commands
{
    public class ApproveMemberCommand : IRequest<bool>
    {
        public int FamilyId { get; set; }
        public int MemberId { get; set; }
        public int AdminUserId { get; set; }
    }
}
