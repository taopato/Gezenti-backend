using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gezenti.Persistence.Contexts.EntityFramwork
{
    public class GezentiDbContext : DbContext
    {
        public GezentiDbContext(DbContextOptions<GezentiDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Place> Places => Set<Place>();
        public DbSet<PlaceCategory> PlaceCategories => Set<PlaceCategory>();
        public DbSet<UserComment> UserComments => Set<UserComment>();
        public DbSet<UserFavorite> UserFavorites => Set<UserFavorite>();
        public DbSet<UserInteraction> UserInteractions => Set<UserInteraction>();
        public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
        public DbSet<UserVisitHistory> UserVisitHistory => Set<UserVisitHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GezentiDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
