using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class PlaceCategoryConfiguration : IEntityTypeConfiguration<PlaceCategory>
    {
        public void Configure(EntityTypeBuilder<PlaceCategory> builder)
        {
            builder.ToTable("PlaceCategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.PlaceId).HasColumnName("PlaceId").IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt);

            builder.HasOne(x => x.Place)
                .WithMany(x => x.PlaceCategories)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PlaceCategories_Places");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.PlaceCategories)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PlaceCategories_Categories");
        }
    }
}
