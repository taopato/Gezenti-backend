using Gezenti.Application.Common;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; } = null!;
        public string ResetToken { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
