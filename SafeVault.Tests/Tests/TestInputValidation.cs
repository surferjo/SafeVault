using NUnit.Framework;
using SafeVault.Helpers;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestForSQLInjection()
        {
            string input = "'; DROP TABLE Users;--";
            string sanitized = InputValidator.SanitizeForStorage(input);
            Assert.That(sanitized.Contains("DROP TABLE"), Is.False, "SQL Injection not sanitized properly");
        }

        [Test]
        public void TestForXSS()
        {
            string input = "<script>alert('XSS')</script>";
            string sanitized = InputValidator.SanitizeForStorage(input);
            Assert.That(sanitized.Contains("<script>"), Is.False, "XSS not sanitized properly");
        }

        [Test]
        public void TestValidUsername()
        {
            Assert.That(InputValidator.IsValidUsername("SafeUser_123"), Is.True);
            Assert.That(InputValidator.IsValidUsername("Bad User!"), Is.False);
        }

        [Test]
        public void TestValidEmail()
        {
            Assert.That(InputValidator.IsValidEmail("user@example.com"), Is.True);
            Assert.That(InputValidator.IsValidEmail("not-an-email"), Is.False);
        }
    }
}
