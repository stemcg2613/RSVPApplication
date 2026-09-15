namespace RSVPApplication
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userName = UserNameEntry.Text ?? "";
            string password = PasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageLabel.Text = "Please enter both a username and password.";
                return;
            }

            // Hard-coded credentials for the project
            if (userName == "McGraw" && password == "Password1")
            {
                MessageLabel.Text = "";

                await Navigation.PushAsync(
                    new HomePage());
            }
            else
            {
                MessageLabel.Text = "Invalid username or password.";
            }
        }

        private async void OnGuestClicked(object sender, EventArgs e)
        {
            MessageLabel.Text = "";

            await Navigation.PushAsync(
                new HomePage());
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            MessageLabel.Text = "";

            await Navigation.PushAsync(
                new CreateAccountPage());
        }
    }
}