using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<ApiResponse<LoggedInUserDto>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
