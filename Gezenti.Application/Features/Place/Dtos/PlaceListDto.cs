namespace Gezenti.Application.Features.Place.Dtos
{
    public class PlaceListDto
    {
        public int Id { get; set; }
        public string PlaceName { get; set; } = null!;
        public string? City { get; set; }
        public string? Region { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public double? AverageRating { get; set; }
        public int? TotalReviews { get; set; }
        public List<string> Categories { get; set; } = new();
    }
}
