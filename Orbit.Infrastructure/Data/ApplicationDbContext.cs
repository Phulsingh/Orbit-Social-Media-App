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

        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<InviteCode> InviteCodes { get; set; }

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

            // ── Family ───────────────────────────────
            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasIndex(f => f.FamilyCode)
                      .IsUnique();

                entity.Property(f => f.FamilyName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(f => f.FamilyCode)
                      .IsRequired()
                      .HasMaxLength(20);

                // Family → Creator (ApplicationUsers)
                entity.HasOne(f => f.Creator)
                      .WithMany()
                      .HasForeignKey(f => f.CreatedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── FamilyMember ─────────────────────────
            modelBuilder.Entity<FamilyMember>(entity =>
            {
                // One user can only join a family once
                entity.HasIndex(fm => new { fm.FamilyId, fm.UserId })
                      .IsUnique();

                // FamilyMember → Family
                entity.HasOne(fm => fm.Family)
                      .WithMany(f => f.FamilyMembers)
                      .HasForeignKey(fm => fm.FamilyId)
                      .OnDelete(DeleteBehavior.Cascade);

                // FamilyMember → User
                entity.HasOne(fm => fm.User)
                      .WithMany(u => u.FamilyMembers)
                      .HasForeignKey(fm => fm.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(fm => fm.Relation)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // ── InviteCode ───────────────────────────
            modelBuilder.Entity<InviteCode>(entity =>
            {
                entity.HasIndex(i => i.Code)
                      .IsUnique();

                entity.Property(i => i.Code)
                      .IsRequired()
                      .HasMaxLength(20);

                // InviteCode → Family
                entity.HasOne(i => i.Family)
                      .WithMany(f => f.InviteCodes)
                      .HasForeignKey(i => i.FamilyId)
                      .OnDelete(DeleteBehavior.Cascade);

                // InviteCode → Creator
                entity.HasOne(i => i.Creator)
                      .WithMany(u => u.InviteCodes)
                      .HasForeignKey(i => i.CreatedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });


        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
            => base.SaveChangesAsync(cancellationToken);


    }
}
