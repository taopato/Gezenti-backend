using Gezenti.Application.Common;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<string>>
    {
        private readonly IUserRepository _userRepository;

        public ForgotPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // ✅ ŞABLON:
            // 1) kullanıcı var mı kontrol et
            // 2) varsa reset token üret
            // 3) mail servise gönder (şimdilik yok)
            // 4) güvenlik için her durumda aynı mesaj dön

            var user = await _userRepository.GetByEmailAsync(request.Email);

            // Mail servisi yok + DB reset tablosu yok => şimdilik sadece standart mesaj
            return ApiResponse<string>.Success(
                "Eğer bu email kayıtlıysa şifre sıfırlama linki gönderilecektir. (Mail servisi entegrasyonu bekleniyor.)",
                200
            );
        }
    }
}
