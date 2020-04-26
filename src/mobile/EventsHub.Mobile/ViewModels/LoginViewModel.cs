using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Services.Client;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventsHub.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private bool areCredentialsInvalid;
        private AuthenticationService authenticationService;
        private readonly INavigationService navigationService;

        public LoginViewModel()
        {
            navigationService = App.NavigationService;
            authenticationService = new AuthenticationService();
            Title = "Login";
            AuthenticateCommand = new Command(async () =>
            {
                AreCredentialsInvalid = !await UserAuthenticated(Email, Password);
                if (AreCredentialsInvalid) return;

                //await navigationService.GoBack();
                await navigationService.NavigateAsync(PageName.MainPage);
            });

            AreCredentialsInvalid = false;
        }

        private async Task<bool> UserAuthenticated(string email, string password)
        {
            if (string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return await authenticationService.Login(email, password);
        }

        public string Email
        {
            get => email;
            set
            {
                if (value == email) return;
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (value == password) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand AuthenticateCommand { get; set; }

        public bool AreCredentialsInvalid
        {
            get => areCredentialsInvalid;
            set
            {
                if (value == areCredentialsInvalid) return;
                areCredentialsInvalid = value;
                OnPropertyChanged(nameof(AreCredentialsInvalid));
            }
        }
    }
}
