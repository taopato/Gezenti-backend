using AutoMapper;
using Gezenti.Application.Features.Auth.Commands;
using Gezenti.Application.Features.Auth.Commands.RegisterUser;
using Gezenti.Application.Features.Auth.Dtos;
using Gezenti.Domain;

namespace Gezenti.Application.Features.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserCommand, User>();
            CreateMap<User, LoggedInUserDto>();
        }
    }
}
