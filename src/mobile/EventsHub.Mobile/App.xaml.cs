using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EventsHub.Mobile.Services;
using EventsHub.Mobile.Views;
using EventsHub.Mobile.Constants;

namespace EventsHub.Mobile
{
    public partial class App : Application
    {
        public static string BackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://xelazardasp-001-site1.itempurl.com" : "http://localhost:5001";
        //DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5001" : "http://localhost:5001";
        public static bool UseMockDataStore = true;
        private static readonly FormsNavigationService navigationService = new FormsNavigationService();

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();

            navigationService.Configure(PageName.LoginPage, typeof(LoginPage));
            navigationService.Configure(PageName.MainPage, typeof(MainPage));

            MainPage = navigationService.SetRootPage(nameof(MainPage));
        }

        public static INavigationService NavigationService { get; } = navigationService;

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
