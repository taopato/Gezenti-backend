using AutoMapper;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Commands;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Application.Services.Repositories;
using Gezenti.Domain.Entities;
using MediatR;
using PlaceEntity = Gezenti.Domain.Entities.Place;

namespace Gezenti.Application.Features.Place.Handler
{
    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, ApiResponse<GetPlaceDetailDto>>
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public CreatePlaceCommandHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetPlaceDetailDto>> Handle(
            CreatePlaceCommand request,
            CancellationToken cancellationToken)
        {
            var place = _mapper.Map<PlaceEntity>(request);

            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                place.PlaceCategories = request.CategoryIds.Select(categoryId => new PlaceCategory
                {
                    CategoryId = categoryId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();
            }

            var result = await _placeService.AddPlaceAsync(place);

            if (!result.Success)
            {
                return ApiResponse<GetPlaceDetailDto>.Fail(
                    result.Message ?? "Yer oluşturulurken bir hata oluştu.",
                    400
                );
            }

            var placeDto = _mapper.Map<GetPlaceDetailDto>(place);
            
            return ApiResponse<GetPlaceDetailDto>.Success(placeDto, 201, "Yer başarıyla oluşturuldu.");
        }
    }
}

