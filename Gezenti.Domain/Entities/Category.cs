using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
        public string? CategoryType { get; set; }
        public string? Description { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public ICollection<PlaceCategory> PlaceCategories { get; set; } = new List<PlaceCategory>();
    }
}
