using Xamarin.Forms;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Views;
using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Repositories;
using System.Windows.Input;

namespace EventsHub.Mobile
{
    public partial class App : Application
    {
        public static string ApiUrl = "http://xelazardasp-001-site1.itempurl.com/api/";
        private static readonly FormsNavigationService navigationService = new FormsNavigationService();
        private static TheatreRepository theatreRepository;
        public const string DATABASE_NAME = "events-hub.db";

        public static ICommand ToLoginPageCommand { get; set; }
        public static ICommand ToMainPageCommand { get; set; }
        public static ICommand ToAboutPageCommand { get; set; }

        public static INavigationService NavigationService { get; } = navigationService;
        public static TheatreRepository Database
        {
            get
            {
                if (theatreRepository == null)
                {
                    theatreRepository = new TheatreRepository(DbHelper.GetDatabasePath(DATABASE_NAME));
                }
                return theatreRepository;
            }
        }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            ConfigureNavigation();
            AddNavigateCommands();
            MainPage = navigationService.SetRootPage(nameof(MainPage));
        }

        private void ConfigureNavigation()
        {
            NavigationService.Configure(PageName.MainPage, typeof(MainPage));
            NavigationService.Configure(PageName.MenuPage, typeof(MenuPage));
            NavigationService.Configure(PageName.ItemsPage, typeof(ItemsPage));
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
