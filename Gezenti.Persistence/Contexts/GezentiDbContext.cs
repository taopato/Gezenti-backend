using Gezenti.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Gezenti.Persistence.Contexts
{
    public class GezentiDbContext : DbContext
    {
        public GezentiDbContext(DbContextOptions<GezentiDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.HasIndex(x => x.Email)
                    .IsUnique();

                builder.Property(x => x.PasswordHash)
                    .IsRequired();

                builder.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
