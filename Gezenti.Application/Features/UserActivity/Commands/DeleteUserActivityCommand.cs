using Gezenti.Application.Common;
using MediatR;

namespace Gezenti.Application.Features.UserActivity.Commands
{
    public class DeleteUserActivityCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
}

