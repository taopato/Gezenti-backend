using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
            builder.ToTable("UserComments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(x => x.PlaceId).HasColumnName("PlaceId").IsRequired();

            builder.Property(x => x.Content).HasColumnName("Content").HasMaxLength(2000);
            builder.Property(x => x.Rating).HasColumnName("Rating");
            builder.Property(x => x.SentimentScore).HasColumnName("SentimentScore");

            builder.Property(x => x.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedDate");

            builder.HasCheckConstraint("CK__UserComme__Ratin__4CA06362", "[Rating] >= 1 AND [Rating] <= 5");

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserComments)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_UserComments_User");

            builder.HasOne(x => x.Place)
                .WithMany(x => x.UserComments)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserComments_Places");
        }
    }
}
