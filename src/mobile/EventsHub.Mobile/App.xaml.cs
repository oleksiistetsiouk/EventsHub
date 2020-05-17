using Xamarin.Forms;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Views;
using EventsHub.Mobile.Constants;
using System.Windows.Input;
using MonkeyCache.FileStore;

namespace EventsHub.Mobile
{
    public partial class App : Application
    {
        public static string ApiUrl = "http://xelazardasp-001-site1.itempurl.com/api/";
        private static readonly FormsNavigationService navigationService = new FormsNavigationService();

        public static ICommand ToLoginPageCommand { get; set; }
        public static ICommand ToMainPageCommand { get; set; }
        public static ICommand ToAboutPageCommand { get; set; }
        public static INavigationService NavigationService { get; } = navigationService;

        public App()
        {
            InitializeComponent();
            ConfigureNavigation();
            AddNavigateCommands();
            MainPage = navigationService.SetRootPage(nameof(MainPage));
            Barrel.ApplicationId = "EventsHub";
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
