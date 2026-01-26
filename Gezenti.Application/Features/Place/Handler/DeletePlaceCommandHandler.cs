using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Commands;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Place.Handler
{
    public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, ApiResponse<bool>>
    {
        private readonly IPlaceService _placeService;

        public DeletePlaceCommandHandler(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        public async Task<ApiResponse<bool>> Handle(
            DeletePlaceCommand request,
            CancellationToken cancellationToken)
        {
            var getResult = await _placeService.GetPlaceByIdAsync(request.Id);
            
            if (!getResult.Success || getResult.Data == null)
            {
                return ApiResponse<bool>.Fail("Silinecek yer bulunamadı.", 404);
            }

            var result = await _placeService.DeletePlaceAsync(request.Id);

            if (!result.Success)
            {
                return ApiResponse<bool>.Fail(
                    result.Message ?? "Yer silinirken bir hata oluştu.",
                    400
                );
            }

            return ApiResponse<bool>.Success(true, 200, "Yer başarıyla silindi.");
        }
    }
}
