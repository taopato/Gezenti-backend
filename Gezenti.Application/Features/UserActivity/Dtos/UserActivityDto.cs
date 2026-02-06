namespace Gezenti.Application.Features.UserActivity.Dtos
{
    public class UserActivityDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PlaceId { get; set; } = null!;
        public string PlaceCategories { get; set; } = null!;
        public string City { get; set; } = null!;
        public double RatingGiven { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

