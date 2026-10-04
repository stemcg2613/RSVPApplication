using RSVPApplication.DataAccess;
using RSVPApplication.Models;

namespace RSVPApplication
{
    public partial class CreateAccountPage : ContentPage
    {
        private readonly DatabaseService databaseService;

        public CreateAccountPage()
        {
            InitializeComponent();

            databaseService = new DatabaseService();
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text?.Trim() ?? "";
            string lastName = LastNameEntry.Text?.Trim() ?? "";
            string email = EmailEntry.Text?.Trim() ?? "";
            string mobilePhone = MobilePhoneEntry.Text?.Trim() ?? "";
            string userName = NewUserNameEntry.Text?.Trim() ?? "";
            string password = NewPasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(mobilePhone) ||
                string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageLabel.Text = "Please complete all fields.";
                return;
            }

            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                MobilePhone = mobilePhone,
                Username = userName,
                Password = password
            };

            try
            {
                await databaseService.AddUserAsync(newUser);

                MessageLabel.Text = "";

                await DisplayAlert(
                    "Account Created",
                    "Your account has been created successfully.",
                    "OK");

                await Navigation.PopAsync();
            }
            catch (SQLite.SQLiteException)
            {
                MessageLabel.Text =
                    "That username is already being used. Please choose another username.";
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}