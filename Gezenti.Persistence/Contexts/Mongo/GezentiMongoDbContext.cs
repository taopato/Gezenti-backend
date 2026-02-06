using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Gezenti.Persistence.Contexts.Mongo
{
    public class GezentiMongoDbContext : DbContext
    {
        public GezentiMongoDbContext(DbContextOptions<GezentiMongoDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserActivity> User_logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserActivity>().ToCollection("User_logs");

            modelBuilder.Entity<UserActivity>()
                .OwnsOne(x => x.Details, details =>
                {
                    details.Property(d => d.RatingGiven)
                        .HasElementName("ratingGiven");
                });

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.Id)
                .HasElementName("_id")
                .ValueGeneratedNever();

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.UserId)
                .HasElementName("userId");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.UserName)
                .HasElementName("userName");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.PlaceId)
                .HasElementName("placeId");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.PlaceCategories)
                .HasElementName("placeCategories");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.City)
                .HasElementName("city");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.CreatedAt)
                .HasElementName("createdAt");

            modelBuilder.Entity<UserActivity>()
                .Property(x => x.UpdatedAt)
                .HasElementName("updatedAt");
        }
    }
}