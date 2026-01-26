using AutoMapper;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Application.Features.Place.Queries;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Place.Handler
{
    public class GetAllPlacesQueryHandler : IRequestHandler<GetAllPlacesQuery, ApiResponse<List<PlaceListDto>>>
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public GetAllPlacesQueryHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<PlaceListDto>>> Handle(
            GetAllPlacesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _placeService.GetAllPlacesAsync();

            if (!result.Success || result.Data == null)
            {
                return ApiResponse<List<PlaceListDto>>.Fail(
                    result.Message ?? "Yerler getirilirken bir hata oluştu.",
                    400
                );
            }

            var placeDtos = _mapper.Map<List<PlaceListDto>>(result.Data);

             return ApiResponse<List<PlaceListDto>>.Success(placeDtos, 200, "Yerler başarıyla getirildi.");
        }
    }
}
