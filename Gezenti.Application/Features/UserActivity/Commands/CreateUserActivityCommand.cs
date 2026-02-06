using Gezenti.Application.Common;
using Gezenti.Application.Features.UserActivity.Dtos;
using MediatR;

namespace Gezenti.Application.Features.UserActivity.Commands
{
    public class CreateUserActivityCommand : IRequest<ApiResponse<UserActivityDto>>
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PlaceId { get; set; } = null!;
        public string PlaceCategories { get; set; } = null!;
        public string City { get; set; } = null!;
        public double RatingGiven { get; set; }
    }
}

