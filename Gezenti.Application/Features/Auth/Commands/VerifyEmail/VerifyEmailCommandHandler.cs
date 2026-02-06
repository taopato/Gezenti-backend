using Gezenti.Application.Common;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, ApiResponse<string>>
    {
        private readonly IUserRepository _userRepository;

        public VerifyEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByEmailAsync(request.Email.Trim());
            if (!userResult.Success || userResult.Data == null)
                return ApiResponse<string>.Fail("Kullanıcı bulunamadı.", 404);

            var user = userResult.Data;

            if (user.EmailConfirmed)
                return ApiResponse<string>.Success("Hesap zaten doğrulanmış.", 200);

            if (string.IsNullOrWhiteSpace(user.EmailVerificationCode) ||
                user.EmailVerificationExpiresAt == null ||
                user.EmailVerificationExpiresAt < DateTime.UtcNow)
            {
                return ApiResponse<string>.Fail("Doğrulama kodu geçersiz veya süresi dolmuş.", 400);
            }

            if (!string.Equals(user.EmailVerificationCode, request.Code.Trim(), StringComparison.Ordinal))
                return ApiResponse<string>.Fail("Doğrulama kodu yanlış.", 400);

            user.EmailConfirmed = true;
            user.EmailVerificationCode = null;
            user.EmailVerificationExpiresAt = null;

            await _userRepository.UpdateAsync(user);

            return ApiResponse<string>.Success("E-posta doğrulandı. Artık giriş yapabilirsiniz.", 200);
        }
    }
}

