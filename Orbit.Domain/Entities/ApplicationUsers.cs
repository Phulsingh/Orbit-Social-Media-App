namespace Orbit.Domain.Entities
{
    public class ApplicationUsers
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string PasswordHash { get; set; }

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
