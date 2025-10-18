namespace SafeVault.Models
{
    public class User
    {
        public int Id { get; set; }

        // basic identity info
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // hashed password
        public string PasswordHash { get; set; } = string.Empty;

        // user role (e.g., Admin / User)
        public string Role { get; set; } = "User";
    }
}
