using Gezenti.Domain.Entities;

namespace Gezenti.Application.Services
{
    public interface ITokenHelper
    {
        string CreateAccessToken(User user);
    }
}
