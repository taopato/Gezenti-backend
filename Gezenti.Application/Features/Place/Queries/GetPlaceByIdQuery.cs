using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using MediatR;

namespace Gezenti.Application.Features.Place.Queries
{
    public class GetPlaceByIdQuery : IRequest<ApiResponse<GetPlaceDetailDto>>
    {
        public int Id { get; set; }
    }
}
