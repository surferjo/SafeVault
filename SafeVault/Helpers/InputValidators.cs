using System.Text.RegularExpressions;

namespace SafeVault.Helpers
{
    public static class InputValidator
    {
        // ✅ Sanitize input from SQLi / XSS
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove SQL keywords
            string sanitized = Regex.Replace(input, @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|ALTER|CREATE|EXEC|UNION)\b)", "", RegexOptions.IgnoreCase);

            // Remove special SQL characters
            sanitized = Regex.Replace(sanitized, @"([';--])", "", RegexOptions.IgnoreCase);

            // Remove HTML / script tags
            sanitized = Regex.Replace(sanitized, @"<.*?>", "", RegexOptions.IgnoreCase);

            return sanitized.Trim();
        }

        public static string SanitizeForStorage(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // Simple replacement example, remove scripts and dangerous chars
            return input.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        // ✅ Validate username (letters, digits, underscores, 3–20 chars)
        public static bool IsValidUsername(string username)
        {
            return !string.IsNullOrEmpty(username) &&
                   Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$");
        }

        // ✅ Validate email
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
