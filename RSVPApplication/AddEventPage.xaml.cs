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
            string eventDescription = EventDescriptionEditor.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(eventName) ||
                string.IsNullOrWhiteSpace(eventDate) ||
                string.IsNullOrWhiteSpace(eventLocation) ||
                string.IsNullOrWhiteSpace(eventDescription))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            if (!DateTime.TryParse(eventDate, out DateTime parsedDate))
            {
                MessageLabel.Text =
                    "Please enter a valid date and time.";

                return;
            }

            Event newEvent = new Event
            {
                EventName = eventName,
                EventDate = parsedDate,
                Location = eventLocation,
                EventDescription = eventDescription,
                HostUserId = currentUser.UserId
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