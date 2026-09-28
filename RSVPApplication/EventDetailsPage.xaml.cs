using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class EventDetailsPage : ContentPage
    {
        private readonly Event selectedEvent;
        private readonly User? currentUser;

        public EventDetailsPage(
            Event eventItem,
            User? user = null)
        {
            InitializeComponent();

            selectedEvent = eventItem;
            currentUser = user;

            EventNameLabel.Text = selectedEvent.EventName;

            EventDateLabel.Text =
                $"Date: {selectedEvent.EventDate:MMMM d, yyyy h:mm tt}";

            EventLocationLabel.Text =
                $"Location: {selectedEvent.Location}";

            EventDescriptionLabel.Text =
                $"Description: {selectedEvent.EventDescription}";
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