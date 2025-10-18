using SafeVault.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;

namespace SafeVault.Data
{
    public class UserRepository
    {
        private static readonly ConcurrentDictionary<string, User> _users = new();

        private readonly SafeVaultContext _context;

        public UserRepository(SafeVaultContext context) // ✅ Add this constructor
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (_users.ContainsKey(user.Username))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _users[user.Username] = user;

            await Task.CompletedTask;
            return true;
        }


        public async Task<bool> InsertUserAsync(User user)
        {
            return await RegisterUserAsync(user);
        }


        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            if (_users.TryGetValue(username, out var user))
            {
                bool valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                return await Task.FromResult(valid ? user : null);
            }

            return await Task.FromResult<User?>(null);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            _users.TryGetValue(username, out var user);
            return await Task.FromResult(user);
        }
    }
}
