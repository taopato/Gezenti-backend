using Gezenti.Domain;

namespace Gezenti.Application.Services
{
    public interface ITokenHelper
    {
        string CreateAccessToken(User user);
    }
}
