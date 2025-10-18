using NUnit.Framework;
using SafeVault.Data;
using SafeVault.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestAuth
    {
        private SafeVaultContext _context = null!;
        private UserRepository _repo = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SafeVaultContext>()
                .UseInMemoryDatabase(databaseName: "TestAuthDB")
                .Options;

            _context = new SafeVaultContext(options);
            _repo = new UserRepository(_context);
        }

        [Test]
        public async Task TestRegisterAndLogin()
        {
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "Pass123",
                Role = "admin"
            };

            await _repo.RegisterUserAsync(adminUser);

            var success = await _repo.AuthenticateAsync("admin", "Pass123");
            Assert.That(success, Is.True);

            var invalid = await _repo.AuthenticateAsync("admin", "wrongpass");
            Assert.That(invalid, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            // Clear database entries
            foreach (var u in _context.Users)
                _context.Users.Remove(u);

            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
