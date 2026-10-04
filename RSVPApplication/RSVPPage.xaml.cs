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

            // Display the selected event
            EventNameLabel.Text = selectedEvent.EventName;

            // Prepopulate the logged-in user's information
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