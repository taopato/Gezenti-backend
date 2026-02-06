using Gezenti.Application.Common;
using Gezenti.Application.Features.UserActivity.Commands;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.UserActivity.Handler
{
    public class DeleteUserActivityCommandHandler : IRequestHandler<DeleteUserActivityCommand, ApiResponse<bool>>
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public DeleteUserActivityCommandHandler(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<ApiResponse<bool>> Handle(
            DeleteUserActivityCommand request,
            CancellationToken cancellationToken)
        {
            var getResult = await _userActivityRepository.GetByIdAsync(request.Id);
            
            if (!getResult.Success || getResult.Data == null)
            {
                return ApiResponse<bool>.Fail("Silinecek kullanıcı aktivitesi bulunamadı.", 404);
            }

            var result = await _userActivityRepository.DeleteAsync(request.Id);

            if (!result.Success)
            {
                return ApiResponse<bool>.Fail(
                    result.Message ?? "Kullanıcı aktivitesi silinirken bir hata oluştu.",
                    400
                );
            }

            return ApiResponse<bool>.Success(true, 200, "Kullanıcı aktivitesi başarıyla silindi.");
        }
    }
}

