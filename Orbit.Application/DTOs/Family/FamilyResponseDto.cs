using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Application.DTOs.Family
{
    public class FamilyResponseDto
    {
        public int Id { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public string FamilyCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public int TotalMembers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
