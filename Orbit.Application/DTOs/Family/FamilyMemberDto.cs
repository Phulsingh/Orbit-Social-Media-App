using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Application.DTOs.Family
{
    public class FamilyMemberDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsApproved { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
