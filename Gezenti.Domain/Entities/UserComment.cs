using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class UserComment : BaseEntity
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public string? Content { get; set; }
        public int? Rating { get; set; }
        public double? SentimentScore { get; set; }

        public User User { get; set; } = null!;
        public Place Place { get; set; } = null!;
    }
}
