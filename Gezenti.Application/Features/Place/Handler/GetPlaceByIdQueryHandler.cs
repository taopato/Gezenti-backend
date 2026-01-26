using AutoMapper;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using Gezenti.Application.Features.Place.Queries;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Place.Handler
{
    public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, ApiResponse<GetPlaceDetailDto>>
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public GetPlaceByIdQueryHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetPlaceDetailDto>> Handle(
            GetPlaceByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _placeService.GetPlaceByIdAsync(request.Id);

            if (!result.Success || result.Data == null)
            {
                return ApiResponse<GetPlaceDetailDto>.Fail(
                    result.Message ?? "Yer bulunamadı.",
                    404
                );
            }

            var placeDto = _mapper.Map<GetPlaceDetailDto>(result.Data);

            return ApiResponse<GetPlaceDetailDto>.Success(placeDto, 200, "Yer detayı başarıyla getirildi.");
        }
    }
}

