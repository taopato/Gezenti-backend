using AutoMapper;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Application.Features.Place.Queries;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Place.Handler
{
    public class GetPlacesByCategoryQueryHandler : IRequestHandler<GetPlacesByCategoryQuery, ApiResponse<List<PlaceListDto>>>
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public GetPlacesByCategoryQueryHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<PlaceListDto>>> Handle(
            GetPlacesByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _placeService.GetPlacesByCategoryAsync(request.CategoryId);

            if (!result.Success || result.Data == null)
            {
                return ApiResponse<List<PlaceListDto>>.Fail(
                    result.Message ?? "Kategoriye ait yerler getirilirken bir hata oluştu.",
                    400
                );
            }

            var placeDtos = _mapper.Map<List<PlaceListDto>>(result.Data);

            return ApiResponse<List<PlaceListDto>>.Success(placeDtos, 200, "Kategoriye ait yerler başarıyla getirildi.");
        }
    }
}

