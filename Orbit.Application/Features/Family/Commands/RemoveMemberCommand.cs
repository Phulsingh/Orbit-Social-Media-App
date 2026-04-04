using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Application.Features.Family.Commands
{
    public class RemoveMemberCommand : IRequest<bool>  
    {
        public int FamilyId { get; set; }
        public int MemberId { get; set; }
        public int AdminUserId { get; set; }
    }
}
