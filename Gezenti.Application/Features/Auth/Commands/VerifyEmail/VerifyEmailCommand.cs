using Gezenti.Application.Common;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}

