using RSVPApplication.DataAccess;

namespace RSVPApplication
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService databaseService;

        public MainPage()
        {
            InitializeComponent();

            databaseService = new DatabaseService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userName = UserNameEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageLabel.Text =
                    "Please enter both a username and password.";

                return;
            }

            var user = await databaseService.LoginAsync(
                userName,
                password);

            if (user != null)
            {
                MessageLabel.Text = "";

                await Navigation.PushAsync(
                    new HomePage(user));
            }
            else
            {
                MessageLabel.Text =
                    "Invalid username or password.";
            }
        }

        private async void OnGuestClicked(object sender, EventArgs e)
        {
            MessageLabel.Text = "";

            await Navigation.PushAsync(
                new HomePage());
        }

        private async void OnCreateAccountClicked(
            object sender,
            EventArgs e)
        {
            MessageLabel.Text = "";

            await Navigation.PushAsync(
                new CreateAccountPage());
        }
    }
}