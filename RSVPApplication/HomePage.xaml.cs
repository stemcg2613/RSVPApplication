namespace RSVPApplication
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnAllEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventListPage("All Events"));
        }

        private async void OnAttendingEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventListPage("Events I'm Attending"));
        }

        private async void OnHostingEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new EventListPage("Events I'm Hosting"));
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new AddEventPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}