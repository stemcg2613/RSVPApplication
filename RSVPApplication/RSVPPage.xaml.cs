using RSVPApplication.DataAccess;
using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class RSVPPage : ContentPage
    {
        private readonly Event selectedEvent;
        private readonly User currentUser;
        private readonly DatabaseService databaseService;

        public RSVPPage(Event eventItem, User user)
        {
            InitializeComponent();

            selectedEvent = eventItem;
            currentUser = user;
            databaseService = new DatabaseService();

            EventNameLabel.Text = selectedEvent.EventName;

            FirstNameEntry.Text = currentUser.FirstName;
            LastNameEntry.Text = currentUser.LastName;
            EmailEntry.Text = currentUser.Email;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text?.Trim() ?? "";
            string lastName = LastNameEntry.Text?.Trim() ?? "";
            string email = EmailEntry.Text?.Trim() ?? "";
            string guestCountText = GuestCountEntry.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(guestCountText))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            if (!int.TryParse(guestCountText, out int guestCount) ||
                guestCount < 0)
            {
                MessageLabel.Text =
                    "Please enter a valid number of guests.";
                return;
            }

            // Check RSVP deadline
            if (selectedEvent.RSVPDeadline != default &&
                DateTime.Now > selectedEvent.RSVPDeadline)
            {
                MessageLabel.Text =
                    "The RSVP deadline for this event has passed.";

                await DisplayAlert(
                    "RSVP Closed",
                    "The RSVP deadline for this event has passed.",
                    "OK");

                return;
            }

            var rsvps = await databaseService.GetRSVPsAsync();

            // Check for duplicate RSVP
            bool alreadyRSVPd = rsvps.Any(r =>
                r.UserId == currentUser.UserId &&
                r.EventId == selectedEvent.EventId &&
                r.Status == "Attending");

            if (alreadyRSVPd)
            {
                MessageLabel.Text =
                    "You have already RSVP'd to this event.";

                await DisplayAlert(
                    "Duplicate RSVP",
                    "You have already RSVP'd to this event.",
                    "OK");

                return;
            }

            // Calculate current attendance.
            // Each RSVP includes the registered user plus their guests.
            int currentAttendance = rsvps
                .Where(r =>
                    r.EventId == selectedEvent.EventId &&
                    r.Status == "Attending")
                .Sum(r => 1 + r.GuestCount);

            int requestedAttendance = 1 + guestCount;

            // Check maximum attendance
            if (selectedEvent.MaximumAttendees > 0 &&
                currentAttendance + requestedAttendance >
                selectedEvent.MaximumAttendees)
            {
                int remainingSpots =
                    Math.Max(
                        0,
                        selectedEvent.MaximumAttendees -
                        currentAttendance);

                MessageLabel.Text =
                    $"Not enough space. Only {remainingSpots} spot(s) remain.";

                await DisplayAlert(
                    "Event Full",
                    $"Only {remainingSpots} spot(s) remain for this event.",
                    "OK");

                return;
            }

            RSVP newRSVP = new RSVP
            {
                UserId = currentUser.UserId,
                EventId = selectedEvent.EventId,
                Status = "Attending",
                GuestCount = guestCount
            };

            await databaseService.AddRSVPAsync(newRSVP);

            MessageLabel.Text = "";

            await DisplayAlert(
                "RSVP Complete",
                $"Your RSVP for {selectedEvent.EventName} has been saved.",
                "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}