using AutoMapper;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Commands;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Application.Services.Repositories;
using Gezenti.Domain.Entities;
using MediatR;

namespace Gezenti.Application.Features.Place.Handler
{
    public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, ApiResponse<GetPlaceDetailDto>>
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public UpdatePlaceCommandHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetPlaceDetailDto>> Handle(
            UpdatePlaceCommand request,
            CancellationToken cancellationToken)
        {
            var getResult = await _placeService.GetPlaceByIdAsync(request.Id);
            
            if (!getResult.Success || getResult.Data == null)
            {
                return ApiResponse<GetPlaceDetailDto>.Fail("Güncellenecek yer bulunamadı.", 404);
            }

            var existingPlace = getResult.Data;

            _mapper.Map(request, existingPlace);

            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                existingPlace.PlaceCategories = request.CategoryIds.Select(categoryId => new PlaceCategory
                {
                    PlaceId = existingPlace.Id,
                    CategoryId = categoryId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();
            }

            var result = await _placeService.UpdatePlaceAsync(existingPlace);

            if (!result.Success)
            {
                return ApiResponse<GetPlaceDetailDto>.Fail(
                    result.Message ?? "Yer güncellenirken bir hata oluştu.",
                    400
                );
            }

            var updatedPlaceDto = _mapper.Map<GetPlaceDetailDto>(existingPlace);

            return ApiResponse<GetPlaceDetailDto>.Success(updatedPlaceDto, 200, "Yer başarıyla güncellendi.");
        }
    }
}

