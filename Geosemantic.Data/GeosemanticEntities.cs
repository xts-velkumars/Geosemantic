using Geosemantic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xen.Entity;

namespace Geosemantic.Data
{
    public class GeosemanticEntities : XenContext
    {
        public GeosemanticEntities(DbContextOptions<GeosemanticEntities> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitializeUserTable(modelBuilder);
            InitializeRoleTable(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void InitializeUserTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(25);
            });
        }

        private static void InitializeRoleTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(c => c.RolePageMapping)
                    .WithOne(e => e.Role)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                // One to Many mapping
                entity.HasMany(c => c.User)
                    .WithOne(e => e.Role)
                    .HasForeignKey(b => b.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<User> User { get; set; }

        public DbSet<Page> Page { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<RolePageMapping> RolePageMapping { get; set; }
    }
}
