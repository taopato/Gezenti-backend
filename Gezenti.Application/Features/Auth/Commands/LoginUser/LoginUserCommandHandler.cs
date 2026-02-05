using System.Security.Cryptography;
using System.Text;
using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<LoggedInUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public LoginUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<ApiResponse<LoggedInUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByEmailAsync(request.Email.Trim());

            if (!userResult.Success || userResult.Data == null)
                return ApiResponse<LoggedInUserDto>.Fail("Kullanıcı bulunamadı.", 400);

            var user = userResult.Data;

            if (!user.EmailConfirmed)
                return ApiResponse<LoggedInUserDto>.Fail("Lütfen e-posta doğrulaması yapın.", 403);

            var passwordOk = VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordOk)
                return ApiResponse<LoggedInUserDto>.Fail("Şifre hatalı.", 400);

            user.LastLoginDate = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var token = _tokenHelper.CreateAccessToken(user);

            var fullName = user.UserName?.Trim() ?? "";
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var dto = new LoggedInUserDto
            {
                Id = user.Id,
                Email = user.UserGmail,
                FirstName = parts.Length > 0 ? parts[0] : fullName,
                LastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : null,
                AccessToken = token
            };

            return ApiResponse<LoggedInUserDto>.Success(dto, 200);
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return CryptographicOperations.FixedTimeEquals(computed, storedHash);
        }
    }
}
