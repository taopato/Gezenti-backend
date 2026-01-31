using System.Security.Cryptography;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Gezenti.Application.Services.Repositories; 
using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Core.Utilities.Concrete; 

namespace Gezenti.Persistence.Service
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string SmtpHost => _configuration["EmailSettings:SmtpHost"];
        private int SmtpPort => int.Parse(_configuration["EmailSettings:SmtpPort"]);
        private string SenderEmail => _configuration["EmailSettings:SenderEmail"];
        private string SenderPassword => _configuration["EmailSettings:SenderPassword"];

        public async Task<IDataResult<string>> SendMail(int userId, string userEmail)
        {
            try
            {
                var verificationCode = GenerateSecureVerificationCode();
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Gezenti Rehberi", SenderEmail));
                message.To.Add(new MailboxAddress("Gezgin", userEmail));
                message.Subject = "Gezenti - Hesap Doğrulama Kodunuz";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 20px auto; border: 1px solid #ddd; border-radius: 8px; overflow: hidden;'>
                            <div style='background-color: #2196F3; color: #ffffff; padding: 20px; text-align: center;'>
                                <h2 style='margin: 0; font-size: 24px;'>Gezenti</h2>
                            </div>
                            <div style='padding: 30px; text-align: center;'>
                                <p style='font-size: 18px; margin-bottom: 25px;'>Yeni yerler keşfetmeye hazır mısın? Hesabını doğrulamak için kodun:</p>
                                <div style='background-color: #f5f5f5; border: 2px solid #2196F3; border-radius: 8px; padding: 15px 10px; display: inline-block;'>
                                    <h1 style='color: #2196F3; margin: 0; font-size: 40px; letter-spacing: 5px;'>{verificationCode}</h1>
                                </div>
                                <p style='margin-top: 25px; font-size: 14px; color: #888;'>Bu kodu kimseyle paylaşmayın. İyi yolculuklar!</p>
                            </div>
                        </div>"
                };

                await ExecuteSendEmail(message);

            
                return new SuccessDataResult<string>(verificationCode, "Doğrulama e-postası başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(null, $"E-posta hatası: {ex.Message}");
            }
        }

        public async Task<IDataResult<string>> ForgetSendMail(int userId, string userEmail)
        {
            try
            {
                var verificationCode = GenerateSecureVerificationCode();
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Gezenti Güvenlik", SenderEmail));
                message.To.Add(new MailboxAddress("Gezgin", userEmail));
                message.Subject = "Gezenti - Şifre Sıfırlama";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <div style='padding: 20px; border: 1px solid #eee;'>
                            <h2 style='color: #f44336;'>Şifre Sıfırlama Talebi</h2>
                            <p>Şifrenizi sıfırlamak için aşağıdaki kodu kullanabilirsiniz:</p>
                            <h1 style='font-size: 32px; color: #f44336;'>{verificationCode}</h1>
                            <p>Eğer bu talebi siz yapmadıysanız lütfen bu e-postayı dikkate almayın.</p>
                        </div>"
                };

                await ExecuteSendEmail(message);

        
                return new SuccessDataResult<string>(verificationCode, "Şifre sıfırlama e-postası gönderildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(null, $"E-posta hatası: {ex.Message}");
            }
        }

        private async Task ExecuteSendEmail(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(SenderEmail, SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string GenerateSecureVerificationCode(int length = 6)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var code = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    byte[] randomNumber = new byte[1];
                    rng.GetBytes(randomNumber);
                    code.Append(randomNumber[0] % 10);
                }
                return code.ToString();
            }
        }
    }
}