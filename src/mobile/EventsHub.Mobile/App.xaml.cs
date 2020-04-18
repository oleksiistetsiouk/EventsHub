using Xamarin.Forms;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Views;
using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Repositories;

namespace EventsHub.Mobile
{
    public partial class App : Application
    {
        public static string BackendUrl = "http://xelazardasp-001-site1.itempurl.com";
        private static readonly FormsNavigationService navigationService = new FormsNavigationService();
        private static TheatreRepository theatreRepository;
        public const string DATABASE_NAME = "events-hub.db";

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

            MainPage = new MainPage();

            navigationService.Configure(PageName.LoginPage, typeof(LoginPage));
            navigationService.Configure(PageName.MainPage, typeof(MainPage));
            MainPage = navigationService.SetRootPage(nameof(MainPage));
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
