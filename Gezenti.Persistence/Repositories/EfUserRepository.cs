using Gezenti.Application.Services.Repositories;
using Gezenti.Core.Utilities.Abstrak;
using Gezenti.Core.Utilities.Concrete;
using Gezenti.Domain.Entities;
using Gezenti.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gezenti.Persistence.Repositories
{
    public class EfUserRepository : IUserRepository
    {
        private readonly GezentiDbContext _context;

        public EfUserRepository(GezentiDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> Query()
        {
            return _context.Users.AsQueryable();
        }

        public async Task<IDataResult<User?>> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
                return new SuccessDataResult<User?>(user, "Kullanıcı başarıyla getirildi.");

            return new ErrorDataResult<User?>(null, "Kullanıcı bulunamadı.");
        }

        public async Task<IDataResult<User?>> GetByEmailAsync(string email)
        {
       
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserGmail == email);
            if (user != null)
                return new SuccessDataResult<User?>(user, "Kullanıcı e-posta adresine göre getirildi.");

            return new ErrorDataResult<User?>(null, "Bu e-posta adresine kayıtlı kullanıcı bulunamadı.");
        }

        public async Task<IResult> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanıcı başarıyla veri tabanına eklendi.");
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanıcı bilgileri güncellendi.");
        }

        public async Task<IResult> DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanıcı başarıyla silindi.");
        }
    }
}