using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using MediatR;
using BCrypt.Net;

namespace Gezenti.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler
        : IRequestHandler<LoginUserCommand, ApiResponse<LoggedInUserDto>>
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
            // Kullanıcı var mı kısımı
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return ApiResponse<LoggedInUserDto>.Fail("Kullanıcı bulunamadı.", 400);

            // Şifre doğru mu kısmı?
            var passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordOk)
                return ApiResponse<LoggedInUserDto>.Fail("Şifre hatalı.", 400);

            var token = _tokenHelper.CreateAccessToken(user);

            var dto = new LoggedInUserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = token
            };

            return ApiResponse<LoggedInUserDto>.Success(dto, 200);
        }
    }
}
