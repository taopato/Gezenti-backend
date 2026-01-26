using Gezenti.Application.Common;
using Gezenti.Application.Features.Place.Dtos;
using MediatR;

namespace Gezenti.Application.Features.Place.Queries
{
    public class GetAllPlacesQuery : IRequest<ApiResponse<List<PlaceListDto>>>
    {
    }
}
