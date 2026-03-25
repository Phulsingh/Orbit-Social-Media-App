

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbit.Domain.Entities
{
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ Auto increment
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public ApplicationUsers user { get; set; } = null!;

        public ApplicationRoles Role { get; set; } = null!;
    }
}
