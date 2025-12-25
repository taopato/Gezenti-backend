using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class Place : BaseEntity
    {
        public string PlaceName { get; set; } = null!;
        public string? City { get; set; }
        public string? Region { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public string? Description { get; set; }

        public double? AverageRating { get; set; }
        public int? TotalReviews { get; set; }

        public ICollection<PlaceCategory> PlaceCategories { get; set; } = new List<PlaceCategory>();
        public ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();
        public ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();
        public ICollection<UserInteraction> UserInteractions { get; set; } = new List<UserInteraction>();
        public ICollection<UserVisitHistory> UserVisitHistory { get; set; } = new List<UserVisitHistory>();
    }
}
