using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class PlaceCategory : BaseEntity
    {
        public int PlaceId { get; set; }
        public int CategoryId { get; set; }

        public Place Place { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
