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
            // ✅ ŞABLON:
            // 1) user'ı email ile bul
            // 2) ResetToken doğrula (şimdilik yok: DB token tablosu / cache / mail servisi)
            // 3) doğrulandıysa şifreyi Hash+Salt ile güncelle
            // 4) kaydet

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return ApiResponse<string>.Fail("Kullanıcı bulunamadı.", 400);

            // ❗ ResetToken doğrulaması için altyapı yok:
            // - Token DB tablosu yok
            // - Cache yok
            // - Mail servisi yok
            // Bu yüzden şimdilik kapalı tutuyoruz.
            return ApiResponse<string>.Fail(
                "Şifre yenileme altyapısı (mail + token doğrulama) henüz hazır değil. Şimdilik sadece şablon oluşturuldu.",
                501
            );

            // Mail/token altyapısı geldiğinde burası açılacak:
            // CreatePasswordHash(request.NewPassword, out var hash, out var salt);
            // user.PasswordHash = hash;
            // user.PasswordSalt = salt;
            // await _userRepository.UpdateAsync(user);
            // return ApiResponse<string>.Success("Şifre güncellendi.", 200);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
