namespace RSVPApplication
{
    public partial class AddEventPage : ContentPage
    {
        public AddEventPage()
        {
            InitializeComponent();
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            string eventName = EventNameEntry.Text ?? "";
            string eventDate = EventDateEntry.Text ?? "";
            string eventLocation = EventLocationEntry.Text ?? "";
            string eventDescription = EventDescriptionEditor.Text ?? "";

            if (string.IsNullOrWhiteSpace(eventName) ||
                string.IsNullOrWhiteSpace(eventDate) ||
                string.IsNullOrWhiteSpace(eventLocation) ||
                string.IsNullOrWhiteSpace(eventDescription))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            MessageLabel.Text = "";

            await DisplayAlert(
                "Event Added",
                "All event information has been entered.",
                "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}