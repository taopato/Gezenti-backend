using Gezenti.Application.Services.Repositories;
using Gezenti.Domain;
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

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
