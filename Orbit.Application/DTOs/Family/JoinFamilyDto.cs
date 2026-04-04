using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Application.DTOs.Family
{
    public class JoinFamilyDto
    {
        public string InviteCode { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
    }
}
