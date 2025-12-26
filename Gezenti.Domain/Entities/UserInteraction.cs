using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class UserInteraction : BaseEntity
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public string? InteractionType { get; set; }
        public int? DurationSeconds { get; set; }
        public DateTime? Timestamp { get; set; }

        public User User { get; set; } = null!;
        public Place Place { get; set; } = null!;
    }
}
