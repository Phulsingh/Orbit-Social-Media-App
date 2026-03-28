using Microsoft.EntityFrameworkCore;
using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;

namespace Orbit.Infrastructure.Data
{
    public class ApplicationDbContext  :DbContext , IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        IQueryable<ApplicationUsers> IApplicationDbContext.Users => Users;
        public DbSet<ApplicationRoles> Roles { get; set; }
        public DbSet<ApplicationUsers> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent duplicate User+Role combination
            modelBuilder.Entity<UserRoles>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            // UserRoles → ApplicationUsers (Many-to-One)
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.user)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(ur => ur.UserId)  // ✅ UserId not Id
                .OnDelete(DeleteBehavior.Cascade);

            // UserRoles → ApplicationRole (Many-to-One)
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)  // ✅ Correct
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUsers constraints
            modelBuilder.Entity<ApplicationUsers>()
                .HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<ApplicationUsers>()
                .HasIndex(s => s.UserName).IsUnique();

            // PasswordResetToken → ApplicationUsers (Many-to-One)
            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
