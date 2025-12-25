using Gezenti.Domain.Entities;

namespace Gezenti.Application.Services.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Query();

        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
