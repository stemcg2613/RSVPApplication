namespace RSVPApplication
{
    public partial class EventListPage : ContentPage
    {
        private string eventType;

        public EventListPage(string type)
        {
            InitializeComponent();

            eventType = type;
            PageTitleLabel.Text = type;
        }

        private async void OnEventOneClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventDetailsPage(
                    "Richmond Food Truck Festival",
                    "October 17, 2026",
                    "Brown's Island",
                    "An afternoon of local food trucks, music, and outdoor activities."));
        }

        private async void OnEventTwoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventDetailsPage(
                    "Fall Golf Tournament",
                    "November 7, 2026",
                    "Independence Golf Club",
                    "A casual fall golf tournament with teams, prizes, and lunch afterward."));
        }

        private async void OnEventThreeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventDetailsPage(
                    "Holiday Lights Night",
                    "December 12, 2026",
                    "Lewis Ginter Botanical Garden",
                    "An evening meetup to walk through the holiday light displays."));
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}