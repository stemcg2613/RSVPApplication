namespace RSVPApplication
{
    public partial class EventDetailsPage : ContentPage
    {
        private string selectedEventName;

        public EventDetailsPage(
            string eventName,
            string eventDate,
            string eventLocation,
            string eventDescription)
        {
            InitializeComponent();

            selectedEventName = eventName;

            EventNameLabel.Text = eventName;
            EventDateLabel.Text = $"Date: {eventDate}";
            EventLocationLabel.Text = $"Location: {eventLocation}";
            EventDescriptionLabel.Text = $"Description: {eventDescription}";
        }

        private async void OnRSVPClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new RSVPPage(selectedEventName));
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}