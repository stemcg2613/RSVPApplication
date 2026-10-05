using RSVPApplication.DataAccess;
using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class AddEventPage : ContentPage
    {
        private readonly DatabaseService databaseService;
        private readonly User currentUser;

        public AddEventPage(User user)
        {
            InitializeComponent();

            currentUser = user;
            databaseService = new DatabaseService();
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            string eventName = EventNameEntry.Text?.Trim() ?? "";
            string eventDate = EventDateEntry.Text?.Trim() ?? "";
            string eventLocation = EventLocationEntry.Text?.Trim() ?? "";
            string maximumAttendeesText =
                MaximumAttendeesEntry.Text?.Trim() ?? "";
            string rsvpDeadlineText =
                RSVPDeadlineEntry.Text?.Trim() ?? "";
            string eventDescription =
                EventDescriptionEditor.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(eventName) ||
                string.IsNullOrWhiteSpace(eventDate) ||
                string.IsNullOrWhiteSpace(eventLocation) ||
                string.IsNullOrWhiteSpace(maximumAttendeesText) ||
                string.IsNullOrWhiteSpace(rsvpDeadlineText) ||
                string.IsNullOrWhiteSpace(eventDescription))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            if (!DateTime.TryParse(eventDate, out DateTime parsedDate))
            {
                MessageLabel.Text =
                    "Please enter a valid event date and time.";
                return;
            }

            if (!int.TryParse(
                    maximumAttendeesText,
                    out int maximumAttendees) ||
                maximumAttendees <= 0)
            {
                MessageLabel.Text =
                    "Please enter a valid maximum number of attendees.";
                return;
            }

            if (!DateTime.TryParse(
                    rsvpDeadlineText,
                    out DateTime parsedDeadline))
            {
                MessageLabel.Text =
                    "Please enter a valid RSVP deadline.";
                return;
            }

            if (parsedDeadline > parsedDate)
            {
                MessageLabel.Text =
                    "The RSVP deadline must be before the event.";
                return;
            }

            Event newEvent = new Event
            {
                EventName = eventName,
                EventDate = parsedDate,
                Location = eventLocation,
                EventDescription = eventDescription,
                HostUserId = currentUser.UserId,
                MaximumAttendees = maximumAttendees,
                RSVPDeadline = parsedDeadline
            };

            await databaseService.AddEventAsync(newEvent);

            MessageLabel.Text = "";

            await DisplayAlert(
                "Event Added",
                "Your event has been saved successfully.",
                "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}