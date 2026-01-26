using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using MediatR;

namespace Gezenti.Application.Features.Place.Queries
{
    public class GetPlacesByCategoryQuery : IRequest<ApiResponse<List<PlaceListDto>>>
    {
        public int CategoryId { get; set; }
    }
}
