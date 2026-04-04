
namespace Orbit.Domain.Entities
{
   public class FamilyMember
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int UserId { get; set; }
        public string Relation { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Family Family { get; set; } = null!;
        public ApplicationUsers User { get; set; } = null!;
    }
}
