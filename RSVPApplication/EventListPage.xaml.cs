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
            await DisplayAlert(
                "Company Picnic",
                "Event details will be displayed here.",
                "OK");
        }

        private async void OnEventTwoClicked(object sender, EventArgs e)
        {
            await DisplayAlert(
                "Fall Festival",
                "Event details will be displayed here.",
                "OK");
        }

        private async void OnEventThreeClicked(object sender, EventArgs e)
        {
            await DisplayAlert(
                "Holiday Party",
                "Event details will be displayed here.",
                "OK");
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}