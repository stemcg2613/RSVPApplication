using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class HomePage : ContentPage
    {
        private readonly User? currentUser;

        public HomePage(User? user = null)
        {
            InitializeComponent();

            currentUser = user;
        }

        private async void OnAllEventsClicked(
            object sender,
            EventArgs e)
        {
            await Navigation.PushAsync(
                new EventListPage(
                    "All Events",
                    currentUser));
        }

        private async void OnAttendingEventsClicked(
            object sender,
            EventArgs e)
        {
            if (currentUser == null)
            {
                await DisplayAlert(
                    "Login Required",
                    "You must be logged in to view events you are attending.",
                    "OK");

                return;
            }

            await Navigation.PushAsync(
                new EventListPage(
                    "Events I'm Attending",
                    currentUser));
        }

        private async void OnHostingEventsClicked(
            object sender,
            EventArgs e)
        {
            if (currentUser == null)
            {
                await DisplayAlert(
                    "Login Required",
                    "You must be logged in to view events you are hosting.",
                    "OK");

                return;
            }

            await Navigation.PushAsync(
                new EventListPage(
                    "Events I'm Hosting",
                    currentUser));
        }

        private async void OnAddEventClicked(
            object sender,
            EventArgs e)
        {
            if (currentUser == null)
            {
                await DisplayAlert(
                    "Login Required",
                    "You must be logged in to add an event.",
                    "OK");

                return;
            }

            await Navigation.PushAsync(
                new AddEventPage(currentUser));
        }

        private async void OnLogoutClicked(
            object sender,
            EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}