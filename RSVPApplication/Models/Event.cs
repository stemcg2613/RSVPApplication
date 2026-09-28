using SQLite;

namespace RSVPApplication.Models
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int EventId { get; set; }

        [NotNull]
        public string EventName { get; set; } = string.Empty;

        [NotNull]
        public string EventDescription { get; set; } = string.Empty;

        [NotNull]
        public DateTime EventDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public int HostUserId { get; set; }
    }
}