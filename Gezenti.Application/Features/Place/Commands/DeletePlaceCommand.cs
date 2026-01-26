using Gezenti.Application.Common;
using MediatR;

namespace Gezenti.Application.Features.Place.Commands
{
    public class DeletePlaceCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
}
