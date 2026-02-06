using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder.ToTable("UserPreferences", t => 
                t.HasCheckConstraint("CK__UserPrefe__Budge__3C69FB99", "[BudgetScore] >= 1 AND [BudgetScore] <= 5"));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.BudgetScore).HasColumnName("BudgetScore");
            builder.Property(x => x.PreferredClimate).HasColumnName("PreferredClimate").HasMaxLength(20);
            builder.Property(x => x.TravelStyle).HasColumnName("TravelStyle").HasMaxLength(50);

            builder.Property(x => x.HistoryCultureWeight).HasColumnName("HistoryCultureWeight");
            builder.Property(x => x.NatureAdventureWeight).HasColumnName("NatureAdventureWeight");
            builder.Property(x => x.GastronomyWeight).HasColumnName("GastronomyWeight");
            builder.Property(x => x.ShoppingWeight).HasColumnName("ShoppingWeight");

            // DB'de UpdatedDate var. BaseEntity UpdatedAt'i oraya map'liyoruz.
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedDate").HasDefaultValueSql("getdate()");
            builder.Ignore(x => x.CreatedAt);

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserPreference)
                .HasForeignKey<UserPreference>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserPreferences_User");
        }
    }
}
