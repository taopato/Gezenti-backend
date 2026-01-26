using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gezenti.Application.Features.Place.Dtos
{
    public class UpdatePlaceDetailDto
    {
        public int Id { get; set; }
        public string PlaceName { get; set; } = null!;
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }

       
        public List<string> Categories { get; set; } = new();
        public int FavoriteCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
