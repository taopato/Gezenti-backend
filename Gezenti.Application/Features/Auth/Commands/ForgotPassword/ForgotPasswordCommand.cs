using Gezenti.Application.Common;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; } = null!;
    }
}
