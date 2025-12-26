using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserInteractionConfiguration : IEntityTypeConfiguration<UserInteraction>
    {
        public void Configure(EntityTypeBuilder<UserInteraction> builder)
        {
            builder.ToTable("UserInteractions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(x => x.PlaceId).HasColumnName("PlaceId").IsRequired();

            builder.Property(x => x.InteractionType).HasColumnName("InteractionType").HasMaxLength(50);
            builder.Property(x => x.DurationSeconds).HasColumnName("DurationSeconds");
            builder.Property(x => x.Timestamp).HasColumnName("Timestamp").HasDefaultValueSql("getdate()");

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserInteractions)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_UserInteractions_User");

            builder.HasOne(x => x.Place)
                .WithMany(x => x.UserInteractions)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserInteractions_Places");
        }
    }
}
