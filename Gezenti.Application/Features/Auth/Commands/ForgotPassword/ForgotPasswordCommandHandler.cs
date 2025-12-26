using Gezenti.Application.Common;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ApiResponse<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService; // Mail servisini içeri alıyoruz

        public ForgotPasswordCommandHandler(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public async Task<ApiResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var userResult = await _userRepository.GetByEmailAsync(request.Email);

   

            if (!userResult.Success || userResult.Data == null)
            {
                return ApiResponse<string>.Fail("Bu e-posta adresine ait bir kullanıcı bulunamadı.", 404);
            }

            var user = userResult.Data;

     
            var mailResult = await _mailService.ForgetSendMail(user.Id, user.UserGmail);

            if (!mailResult.Success)
            {
                return ApiResponse<string>.Fail("E-posta gönderilirken teknik bir sorun oluştu.", 500);
            }

            // 3) MailManager'dan dönen 6 haneli kodu kullanıcıya kaydet
            // Artık null olabilen ResetCode alanını kullanıyoruz
            user.ResetCode = mailResult.Data;

            // 4) Kullanıcıyı güncelle (DB'ye kodu yazdık)
            await _userRepository.UpdateAsync(user);

            return ApiResponse<string>.Success(
                "Şifre sıfırlama kodunuz başarıyla gönderildi. Lütfen e-posta adresinizi kontrol edin.",
                200
            );
        }
    }
}