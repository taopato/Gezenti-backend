namespace Gezenti.Application.Features.Place.Dtos
{
    public class CreatePlaceDto
    {
        public string PlaceName { get; set; } = null!;
        public string? City { get; set; }
        public string? Region { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }
}
