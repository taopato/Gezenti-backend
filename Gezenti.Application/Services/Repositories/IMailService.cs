using Gezenti.Core.Utilities.Abstrak;

namespace Gezenti.Application.Services.Repositories
{
    public interface IMailService
    {
        Task<IDataResult<string>> SendMail(int userId, string userEmail);        // Email doğrulama
        Task<IDataResult<string>> ForgetSendMail(int userId, string userEmail); // Şifre sıfırlama
    }
}
