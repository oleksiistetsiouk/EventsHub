using Xamarin.Forms;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Views;
using EventsHub.Mobile.Constants;
using System.Windows.Input;
using MonkeyCache.FileStore;
using EventsHub.Mobile.Services.Client;
using System.Threading.Tasks;

namespace EventsHub.Mobile
{
    public partial class App : Application
    {
        private static readonly FormsNavigationService navigationService = new FormsNavigationService();
        private ICommand RegisterApp => new Command(async () => await RegisterMobileApp());
        private AuthenticationService authenticationService = new AuthenticationService();
        public static ICommand ToLoginPageCommand { get; set; }
        public static ICommand ToMainPageCommand { get; set; }
        public static ICommand ToAboutPageCommand { get; set; }
        public static INavigationService NavigationService { get; } = navigationService;

        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "EventsHub";
            ConfigureNavigation();
            AddNavigateCommands();
            MainPage = navigationService.SetRootPage(nameof(MainPage));
            RegisterApp.Execute(null);
        }

        private async Task RegisterMobileApp()
        {
            await authenticationService.Login(AppConstants.SERVICE_ACCOUNT_EMAIL, AppConstants.SERVICE_ACCOUNT_PASSWORD);
        }

        private void ConfigureNavigation()
        {
            NavigationService.Configure(PageName.MainPage, typeof(MainPage));
            NavigationService.Configure(PageName.MenuPage, typeof(MenuPage));
            NavigationService.Configure(PageName.LoginPage, typeof(LoginPage));
            NavigationService.Configure(PageName.AboutPage, typeof(AboutPage));
        }

        private void AddNavigateCommands()
        {
            ToLoginPageCommand = new Command(async () => { await navigationService.NavigateAsync(PageName.LoginPage); });
            ToMainPageCommand = new Command(async () => { await navigationService.NavigateAsync(PageName.MainPage); });
            ToAboutPageCommand = new Command(async () => { await navigationService.NavigateAsync(PageName.AboutPage); });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
