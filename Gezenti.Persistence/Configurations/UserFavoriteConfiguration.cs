using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
    {
        public void Configure(EntityTypeBuilder<UserFavorite> builder)
        {
            builder.ToTable("UserFavorites");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(x => x.PlaceId).HasColumnName("PlaceId").IsRequired();

            builder.Property(x => x.AddedDate).HasColumnName("AddedDate").HasDefaultValueSql("getdate()");

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserFavorites)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_UserFavorites_User");

            builder.HasOne(x => x.Place)
                .WithMany(x => x.UserFavorites)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserFavorites_Places");
        }
    }
}
