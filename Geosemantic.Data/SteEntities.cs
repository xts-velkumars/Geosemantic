using Geosemantic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xen.Entity;

namespace Geosemantic.Data
{
    public class SteEntities : XenContext
    {
        public SteEntities(DbContextOptions<SteEntities> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50);

            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }

    }
}
