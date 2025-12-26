using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserVisitHistoryConfiguration : IEntityTypeConfiguration<UserVisitHistory>
    {
        public void Configure(EntityTypeBuilder<UserVisitHistory> builder)
        {
            builder.ToTable("UserVisitHistory");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(x => x.PlaceId).HasColumnName("PlaceId").IsRequired();

            builder.Property(x => x.VisitDate).HasColumnName("VisitDate").HasColumnType("date");
            builder.Property(x => x.StayDuration).HasColumnName("StayDuration");

            builder.Property(x => x.WasPlanned).HasColumnName("WasPlanned").HasDefaultValue(false);

            // DB’de CreatedDate var -> BaseEntity.CreatedAt map
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("getdate()");
            builder.Ignore(x => x.UpdatedAt);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserVisitHistory)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_UserVisitHistory_User");

            builder.HasOne(x => x.Place)
                .WithMany(x => x.UserVisitHistory)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserVisitHistory_Places");
        }
    }
}
