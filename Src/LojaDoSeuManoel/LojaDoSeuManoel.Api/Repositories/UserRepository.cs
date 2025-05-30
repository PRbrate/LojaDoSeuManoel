using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories.Context;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaDoSeuManoel.Api.Repositories
{
    public class UserRepository(LojaDoSeuManoelContext context) : IUserRepository
    {
        private readonly LojaDoSeuManoelContext _context = context;

        public void Dispose() => _context.Dispose();

        public async Task<User> GetFindByEmailAsync(string email)
        {
            var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserFromId(string id)
        {
            var user = await _context.Users
                        .Include(u => u.Requesteds)
                        .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> Update(User user)
        {
            _context.Update(user);

            return await _context.SaveChangesAsync() > 0;

        }
    }
}
