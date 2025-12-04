using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ApiResponse<LoggedInUserDto>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
