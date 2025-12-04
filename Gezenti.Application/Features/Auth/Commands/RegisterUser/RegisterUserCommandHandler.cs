using Gezenti.Application.Common;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Application.Services;
using Gezenti.Application.Services.Repositories;
using Gezenti.Domain;
using MediatR;

namespace Gezenti.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, ApiResponse<LoggedInUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public RegisterUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<ApiResponse<LoggedInUserDto>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            // 1) Email var mı?
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing != null)
                return ApiResponse<LoggedInUserDto>.Fail("Bu email ile kayıtlı kullanıcı zaten var.", 400);

            // 2) Kullanıcı oluştur
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            var token = _tokenHelper.CreateAccessToken(user);

            var dto = new LoggedInUserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = token
            };

            return ApiResponse<LoggedInUserDto>.Success(dto, 201, "Kullanıcı başarıyla kayıt oldu.");
        }
    }
}
