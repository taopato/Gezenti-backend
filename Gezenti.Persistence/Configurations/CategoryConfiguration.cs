using Gezenti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gezenti.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("CategoryId");

            builder.Property(x => x.CategoryName).HasColumnName("CategoryName").HasMaxLength(100).IsRequired();
            builder.Property(x => x.ParentCategoryId).HasColumnName("ParentCategoryId");
            builder.Property(x => x.CategoryType).HasColumnName("CategoryType").HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(500);

            // BaseEntity -> CreatedAt/UpdatedAt map’leri (bu tabloda yoksa ignore edebiliriz)
            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt);

            builder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .HasConstraintName("FK_Categories_Parent");
        }
    }
}
