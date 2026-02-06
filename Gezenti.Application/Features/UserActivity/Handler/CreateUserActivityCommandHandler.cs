using Gezenti.Application.Common;
using Gezenti.Application.Features.UserActivity.Commands;
using Gezenti.Application.Features.UserActivity.Dtos;
using Gezenti.Application.Services.Repositories;
using Gezenti.Domain.Entities;
using MediatR;
using UserActivityEntity = Gezenti.Domain.Entities.UserActivity;

namespace Gezenti.Application.Features.UserActivity.Handler
{
    public class CreateUserActivityCommandHandler : IRequestHandler<CreateUserActivityCommand, ApiResponse<UserActivityDto>>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public CreateUserActivityCommandHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<ApiResponse<UserActivityDto>> Handle(
            CreateUserActivityCommand request,
            CancellationToken cancellationToken)
        {
            var activity = new UserActivityEntity
            {
                UserId = request.UserId,
                UserName = request.UserName,
                PlaceId = request.PlaceId,
                PlaceCategories = request.PlaceCategories,
                City = request.City,
                Details = new ActivityDetails { RatingGiven = request.RatingGiven }
            };

            var result = await _userActivityRepository.AddAsync(activity);

            if (!result.Success)
            {
                return ApiResponse<UserActivityDto>.Fail(
                    result.Message ?? "Kullanıcı aktivitesi oluşturulurken bir hata oluştu.",
                    400
                );
            }

            var activityDto = new UserActivityDto
            {
                Id = activity.Id,
                UserId = activity.UserId,
                UserName = activity.UserName,
                PlaceId = activity.PlaceId,
                PlaceCategories = activity.PlaceCategories,
                City = activity.City,
                RatingGiven = activity.Details.RatingGiven,
                CreatedAt = activity.CreatedAt,
                UpdatedAt = activity.UpdatedAt
            };

            return ApiResponse<UserActivityDto>.Success(activityDto, 201, "Kullanıcı aktivitesi başarıyla oluşturuldu.");
        }
    }
}

