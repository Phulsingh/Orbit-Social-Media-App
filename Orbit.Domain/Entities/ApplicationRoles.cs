
namespace Orbit.Domain.Entities
{
    public class ApplicationRoles
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }


        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
