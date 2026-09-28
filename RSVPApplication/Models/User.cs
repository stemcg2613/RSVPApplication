using SQLite;

namespace RSVPApplication.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [NotNull]
        public string FirstName { get; set; } = string.Empty;

        [NotNull]
        public string LastName { get; set; } = string.Empty;

        [NotNull, Unique]
        public string Username { get; set; } = string.Empty;

        [NotNull]
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}