using System.Security.Cryptography;
using System.Text;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using Gezenti.Domain.Entities;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<LoggedInUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMailService _mailService;

        public RegisterUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMailService mailService)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _mailService = mailService;
        }

        public async Task<ApiResponse<LoggedInUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingResult = await _userRepository.GetByEmailAsync(request.Email);
            if (existingResult.Success && existingResult.Data != null)
                return ApiResponse<LoggedInUserDto>.Fail("Bu email ile kayıtlı kullanıcı zaten var.", 400);

            CreatePasswordHash(request.Password, out var hash, out var salt);

            var user = new User
            {
                UserGmail = request.Email.Trim(),
                UserName = $"{request.FirstName} {request.LastName}".Trim(),
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow,

                EmailConfirmed = false,
                EmailVerificationCode = null,
                EmailVerificationExpiresAt = null
            };

            await _userRepository.AddAsync(user);

            // Doğrulama maili gönderme kısmı 
            var mailResult = await _mailService.SendMail(user.Id, user.UserGmail);
            if (!mailResult.Success || string.IsNullOrWhiteSpace(mailResult.Data))
            {
                return ApiResponse<LoggedInUserDto>.Fail("Kayıt yapıldı ancak doğrulama maili gönderilemedi.", 500);
            }

            user.EmailVerificationCode = mailResult.Data;
            user.EmailVerificationExpiresAt = DateTime.UtcNow.AddMinutes(10);
            await _userRepository.UpdateAsync(user);

            var token = _tokenHelper.CreateAccessToken(user);

            var dto = new LoggedInUserDto
            {
                Id = user.Id,
                Email = user.UserGmail,
                FirstName = request.FirstName,
                LastName = request.LastName,
                AccessToken = token
            };

            return ApiResponse<LoggedInUserDto>.Success(dto, 201,
                "Kayıt başarılı. E-postanıza gelen doğrulama kodu ile hesabınızı doğrulayın.");
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
