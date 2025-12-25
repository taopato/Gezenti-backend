using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class UserFavorite : BaseEntity
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public DateTime? AddedDate { get; set; }

        public User User { get; set; } = null!;
        public Place Place { get; set; } = null!;
    }
}
