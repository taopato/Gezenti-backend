using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class UserPreference : BaseEntity
    {
        public int UserId { get; set; }

        public int? BudgetScore { get; set; }
        public string? PreferredClimate { get; set; }
        public string? TravelStyle { get; set; }

        public double? HistoryCultureWeight { get; set; }
        public double? NatureAdventureWeight { get; set; }
        public double? GastronomyWeight { get; set; }
        public double? ShoppingWeight { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public User User { get; set; } = null!;
    }
}
