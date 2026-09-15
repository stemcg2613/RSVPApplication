namespace RSVPApplication
{
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text ?? "";
            string lastName = LastNameEntry.Text ?? "";
            string userName = NewUserNameEntry.Text ?? "";
            string password = NewPasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            MessageLabel.Text = "";

            await DisplayAlert(
                "Account Created",
                "Your account has been created successfully.",
                "OK");

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}