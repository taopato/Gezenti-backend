using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserGmail).HasColumnName("UserGmail").HasMaxLength(255).IsRequired();

            builder.HasIndex(x => x.UserGmail).IsUnique();

            builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varbinary(max)").IsRequired();
            builder.Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").HasColumnType("varbinary(max)").IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnName("CreatedDate").HasDefaultValueSql("getdate()");
            builder.Property(x => x.LastLoginDate).HasColumnName("LastLoginDate");
            builder.Ignore(x => x.UpdatedAt);
        }
    }
}
