using Domain.Models.Enities.UserEnities;
using Domain.Models.Interfaces;

using MongoDB.Driver;

namespace LoLStatsMaui.Infrastructure.Repositories
{
    public class UserDbRepository : IUserDbRepository
    {
        private readonly LolStatsDbContext _context;

        public UserDbRepository(LolStatsDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var result = await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
            return result != null;
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }
    }
}