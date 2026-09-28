using SQLite;

namespace RSVPApplication.Models
{
    public class RSVP
    {
        [PrimaryKey, AutoIncrement]
        public int RSVPId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}