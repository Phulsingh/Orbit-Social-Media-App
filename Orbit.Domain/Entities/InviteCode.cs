using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbit.Domain.Entities
{
    public class InviteCode
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int CreatedBy { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public int? UsedBy { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Family Family { get; set; } = null!;
        public ApplicationUsers Creator { get; set; } = null!;
    }
}
