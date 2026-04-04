

namespace Orbit.Domain.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public string FamilyCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ApplicationUsers Creator { get; set; } = null!;
        public ICollection<FamilyMember> FamilyMembers { get; set; }
            = new List<FamilyMember>();
        public ICollection<InviteCode> InviteCodes { get; set; }
            = new List<InviteCode>();
    }
}
