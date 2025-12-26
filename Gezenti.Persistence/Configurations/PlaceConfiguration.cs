using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Places");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PlaceId");

            builder.Property(x => x.PlaceName).HasColumnName("PlaceName").HasMaxLength(200).IsRequired();
            builder.Property(x => x.City).HasColumnName("City").HasMaxLength(100);
            builder.Property(x => x.Region).HasColumnName("Region").HasMaxLength(100);

            builder.Property(x => x.Latitude).HasColumnName("Latitude").HasColumnType("decimal(10,8)");
            builder.Property(x => x.Longitude).HasColumnName("Longitude").HasColumnType("decimal(11,8)");

            builder.Property(x => x.Description).HasColumnName("Description");

            builder.Property(x => x.AverageRating).HasColumnName("AverageRating").HasDefaultValue(0.0);
            builder.Property(x => x.TotalReviews).HasColumnName("TotalReviews").HasDefaultValue(0);

            // DB’de CreatedDate var -> BaseEntity.CreatedAt map
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("getdate()");
            builder.Ignore(x => x.UpdatedAt);
        }
    }
}
