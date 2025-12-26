using Gezenti.Domain.Entities;
using Gezenti.Core.Utilities.Abstrak;

namespace Gezenti.Application.Services.Repositories
{
    public interface IUserRepository
    {

        IQueryable<User> Query();

   
        Task<IDataResult<User?>> GetByIdAsync(int id);
        Task<IDataResult<User?>> GetByEmailAsync(string email);


        Task<IResult> AddAsync(User user);
        Task<IResult> UpdateAsync(User user);
        Task<IResult> DeleteAsync(User user);
    }
}