namespace RSVPApplication
{
    public partial class RSVPPage : ContentPage
    {
        private string selectedEventName;

        public RSVPPage(string eventName)
        {
            InitializeComponent();

            selectedEventName = eventName;
            EventNameLabel.Text = eventName;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text ?? "";
            string lastName = LastNameEntry.Text ?? "";
            string email = EmailEntry.Text ?? "";
            string guestCount = GuestCountEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(guestCount))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            MessageLabel.Text = "";

            await DisplayAlert(
                "RSVP Complete",
                $"Your RSVP for {selectedEventName} has been entered.",
                "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}