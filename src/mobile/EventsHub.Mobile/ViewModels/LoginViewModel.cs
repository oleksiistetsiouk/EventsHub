using EventsHub.Mobile.Services;
using EventsHub.Mobile.Services.Client;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventsHub.Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string email;
        private string password;
        private bool areCredentialsInvalid;
        private AuthenticationService authenticationService;

        public LoginViewModel(INavigationService navigationService)
        {
            authenticationService = new AuthenticationService();

            AuthenticateCommand = new Command(async () =>
            {
                AreCredentialsInvalid = !await UserAuthenticated(Email, Password);
                if (AreCredentialsInvalid) return;

                await navigationService.GoBack();
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
            var token = await authenticationService.Login(email, password);
            var isAuthenticated = !string.IsNullOrEmpty(token);

            return isAuthenticated;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
