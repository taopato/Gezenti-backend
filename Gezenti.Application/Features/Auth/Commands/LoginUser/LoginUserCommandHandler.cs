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

        public async Task<ApiResponse<LoggedInUserDto>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByEmailAsync(request.Email);

            if (!userResult.Success || userResult.Data == null)
            {
                return ApiResponse<LoggedInUserDto>.Fail("Kullanıcı bulunamadı.", 400);
            }

            var user = userResult.Data;

            var passwordOk = VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordOk)
            {
                return ApiResponse<LoggedInUserDto>.Fail("Şifre hatalı.", 400);
            }

            var token = _tokenHelper.CreateAccessToken(user);

            var dto = new LoggedInUserDto
            {
                Id = user.Id,
                Email = user.UserGmail,
                FirstName = user.UserName,
                LastName = null,
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