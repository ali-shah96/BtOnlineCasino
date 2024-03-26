using Microsoft.EntityFrameworkCore;
using OnlineCasino.DatabaseContext;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Repositories.Interfaces;

namespace OnlineCasino.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly OnlineCasinoContext _context;

        public UsersRepository(OnlineCasinoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Users> CreateUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(int id, Users user)
        {
            if (id != user.Id)
                return false;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool IsValidCredentials(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            return user != null;
        }
    }
}
