using System.Security.Cryptography;
using System.Text;
using Gezenti.Application.Common;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<string>>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByEmailAsync(request.Email.Trim());
            if (!userResult.Success || userResult.Data == null)
                return ApiResponse<string>.Fail("Kullanıcı bulunamadı.", 404);

            var user = userResult.Data;

            if (string.IsNullOrWhiteSpace(user.ResetCode) ||
                user.ResetCodeExpiresAt == null ||
                user.ResetCodeExpiresAt < DateTime.UtcNow)
            {
                return ApiResponse<string>.Fail("Sıfırlama kodu geçersiz veya süresi dolmuş.", 400);
            }

            if (!string.Equals(user.ResetCode, request.ResetToken.Trim(), StringComparison.Ordinal))
                return ApiResponse<string>.Fail("Sıfırlama kodu yanlış.", 400);

            CreatePasswordHash(request.NewPassword, out var hash, out var salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.ResetCode = null;
            user.ResetCodeExpiresAt = null;

            await _userRepository.UpdateAsync(user);

            return ApiResponse<string>.Success("Şifre başarıyla güncellendi.", 200);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
