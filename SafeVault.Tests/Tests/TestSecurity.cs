using NUnit.Framework;
using SafeVault.Data;
using SafeVault.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestSecurity
    {
        private SafeVaultContext _context = null!;
        private UserRepository _repo = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SafeVaultContext>()
                .UseInMemoryDatabase(databaseName: "SecTestDb")
                .Options;

            _context = new SafeVaultContext(options);
            _repo = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var u in _context.Users)
                _context.Users.Remove(u);

            _context.SaveChanges();
            _context.Dispose();
        }

        [Test]
        public async Task SqlInjectionAttempt_IsStoredSafelyOrRejected()
        {
            string payload = "'; DROP TABLE Users;--";

            var attackerUser = new User
            {
                Username = payload,
                Email = "attacker@example.com",
                PasswordHash = "hash",
                Role = "user"
            };

            await _repo.RegisterUserAsync(attackerUser);

            var found = await _repo.GetByUsernameAsync(payload);

            Assert.That(found == null || found.Username == payload, Is.True);
        }

        [Test]
        public async Task XssAttempt_ResponseIsHtmlEncoded()
        {
            string xss = "<script>alert('x')</script>";

            var xssUser = new User
            {
                Username = xss,
                Email = "xss@example.com",
                PasswordHash = "hash",
                Role = "user"
            };

            await _repo.RegisterUserAsync(xssUser);

            using var appFactory = new WebApplicationFactory<Program>();
            var client = appFactory.CreateClient();

            var response = await client.GetAsync($"/User/profile/{WebUtility.UrlEncode(xss)}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var html = await response.Content.ReadAsStringAsync();

            Assert.That(html.Contains("<script>"), Is.False, "Response contains raw script tag -> XSS vulnerability");
            Assert.That(html.Contains("&lt;script&gt;") || html.Contains("&lt;"), Is.True);
        }
    }
}
