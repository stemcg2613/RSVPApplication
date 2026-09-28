using RSVPApplication.DataAccess;
using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class EventListPage : ContentPage
    {
        private readonly string eventType;
        private readonly User? currentUser;
        private readonly DatabaseService databaseService;

        public EventListPage(string type, User? user = null)
        {
            InitializeComponent();

            eventType = type;
            currentUser = user;
            databaseService = new DatabaseService();

            PageTitleLabel.Text = type;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadEventsAsync();
        }

        private async Task LoadEventsAsync()
        {
            List<Event> allEvents =
                await databaseService.GetEventsAsync();

            if (eventType == "All Events")
            {
                EventsCollectionView.ItemsSource = allEvents;
            }
            else if (eventType == "Events I'm Hosting")
            {
                if (currentUser == null)
                {
                    EventsCollectionView.ItemsSource =
                        new List<Event>();

                    return;
                }

                EventsCollectionView.ItemsSource =
                    allEvents
                        .Where(e => e.HostUserId == currentUser.UserId)
                        .ToList();
            }
            else if (eventType == "Events I'm Attending")
            {
                if (currentUser == null)
                {
                    EventsCollectionView.ItemsSource =
                        new List<Event>();

                    return;
                }

                List<RSVP> allRSVPs =
                    await databaseService.GetRSVPsAsync();

                List<int> attendingEventIds =
                    allRSVPs
                        .Where(r =>
                            r.UserId == currentUser.UserId &&
                            r.Status == "Attending")
                        .Select(r => r.EventId)
                        .ToList();

                EventsCollectionView.ItemsSource =
                    allEvents
                        .Where(e =>
                            attendingEventIds.Contains(e.EventId))
                        .ToList();
            }
        }

        private async void OnEventSelected(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault()
                is not Event selectedEvent)
            {
                return;
            }

            EventsCollectionView.SelectedItem = null;

            await Navigation.PushAsync(
                new EventDetailsPage(
                    selectedEvent,
                    currentUser));
        }

        private async void OnGoBackClicked(
            object sender,
            EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}