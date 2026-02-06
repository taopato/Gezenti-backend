using Gezenti.Application.Common;
using Gezenti.Application.Features.UserActivity.Commands;
using Gezenti.Application.Features.UserActivity.Dtos;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.UserActivity.Handler
{
    public class UpdateUserActivityCommandHandler : IRequestHandler<UpdateUserActivityCommand, ApiResponse<UserActivityDto>>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public UpdateUserActivityCommandHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<ApiResponse<UserActivityDto>> Handle(
            UpdateUserActivityCommand request,
            CancellationToken cancellationToken)
        {
            var existingResult = await _userActivityRepository.GetByIdAsync(request.Id);
            
            if (!existingResult.Success || existingResult.Data == null)
            {
                return ApiResponse<UserActivityDto>.Fail("Kullanıcı aktivitesi bulunamadı.", 404);
            }

            var activity = existingResult.Data;
            activity.UserId = request.UserId;
            activity.UserName = request.UserName;
            activity.PlaceId = request.PlaceId;
            activity.PlaceCategories = request.PlaceCategories;
            activity.City = request.City;
            activity.Details.RatingGiven = request.RatingGiven;

            var result = await _userActivityRepository.UpdateAsync(activity);

            if (!result.Success)
            {
                return ApiResponse<UserActivityDto>.Fail(
                    result.Message ?? "Kullanıcı aktivitesi güncellenirken bir hata oluştu.",
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

            return ApiResponse<UserActivityDto>.Success(activityDto, 200, "Kullanıcı aktivitesi başarıyla güncellendi.");
        }
    }
}

