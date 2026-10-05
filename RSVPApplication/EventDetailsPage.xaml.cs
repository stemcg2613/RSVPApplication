using RSVPApplication.DataAccess;
using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class EventDetailsPage : ContentPage
    {
        private readonly Event selectedEvent;
        private readonly User? currentUser;
        private readonly DatabaseService databaseService;

        public EventDetailsPage(
            Event eventItem,
            User? user = null)
        {
            InitializeComponent();

            selectedEvent = eventItem;
            currentUser = user;
            databaseService = new DatabaseService();

            EventNameLabel.Text = selectedEvent.EventName;

            EventDateLabel.Text =
                $"Date: {selectedEvent.EventDate:MMMM d, yyyy h:mm tt}";

            EventLocationLabel.Text =
                $"Location: {selectedEvent.Location}";

            EventDescriptionLabel.Text =
                selectedEvent.EventDescription;

            MaximumAttendeesLabel.Text =
                selectedEvent.MaximumAttendees > 0
                    ? $"Maximum Attendees: {selectedEvent.MaximumAttendees}"
                    : "Maximum Attendees: Not Set";

            RSVPDeadlineLabel.Text =
                selectedEvent.RSVPDeadline != default
                    ? $"RSVP Deadline: {selectedEvent.RSVPDeadline:MMMM d, yyyy h:mm tt}"
                    : "RSVP Deadline: Not Set";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadEventDetailsAsync();
        }

        private async Task LoadEventDetailsAsync()
        {
            var users = await databaseService.GetUsersAsync();
            var rsvps = await databaseService.GetRSVPsAsync();

            // Display host name
            var host = users.FirstOrDefault(
                u => u.UserId == selectedEvent.HostUserId);

            HostNameLabel.Text = host != null
                ? $"Hosted By: {host.FirstName} {host.LastName}"
                : "Hosted By: Unknown";

            // Get all attending RSVPs for this event
            var eventRSVPs = rsvps
                .Where(r =>
                    r.EventId == selectedEvent.EventId &&
                    r.Status == "Attending")
                .ToList();

            // Count each registered user plus their guests
            int currentAttendance = eventRSVPs
                .Sum(r => 1 + r.GuestCount);

            CurrentAttendeesLabel.Text =
                $"Current Attendees: {currentAttendance}";

            // Display attendee names
            var attendeeNames = new List<string>();

            foreach (var rsvp in eventRSVPs)
            {
                var attendee = users.FirstOrDefault(
                    u => u.UserId == rsvp.UserId);

                if (attendee != null)
                {
                    string attendeeText =
                        $"{attendee.FirstName} {attendee.LastName}";

                    if (rsvp.GuestCount > 0)
                    {
                        attendeeText +=
                            $" (+{rsvp.GuestCount} guest(s))";
                    }

                    attendeeNames.Add(attendeeText);
                }
            }

            AttendeeNamesLabel.Text =
                attendeeNames.Count > 0
                    ? string.Join("\n", attendeeNames)
                    : "No attendees yet.";
        }

        private async void OnRSVPClicked(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                await DisplayAlert(
                    "Login Required",
                    "You must be logged in to RSVP to an event.",
                    "OK");

                return;
            }

            await Navigation.PushAsync(
                new RSVPPage(
                    selectedEvent,
                    currentUser));
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}