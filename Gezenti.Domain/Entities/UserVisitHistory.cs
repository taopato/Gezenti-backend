using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class UserVisitHistory : BaseEntity
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public DateTime? VisitDate { get; set; }
        public int? StayDuration { get; set; }
        public bool? WasPlanned { get; set; }

        public DateTime? CreatedDate { get; set; }

        public User User { get; set; } = null!;
        public Place Place { get; set; } = null!;
    }
}
