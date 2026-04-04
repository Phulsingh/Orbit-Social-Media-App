using MediatR;
using Orbit.Application.DTOs.Family;

namespace Orbit.Application.Features.Family.Queries
{
    public class GetMembersQuery : IRequest<List<FamilyMemberDto>>
    {
        public int FamilyId { get; set; }
        public int UserId { get; set; }
    }
}
