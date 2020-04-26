using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace EventsHub.Mobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private MenuPage menuPage;

        public MainPage()
        {
            InitializeComponent();
            menuPage = new MenuPage();
            MasterBehavior = MasterBehavior.Popover;
            Master = menuPage;
            var displayPage = new ItemsPage();
            Detail = new NavigationPage(displayPage);

            menuPage.menu.ItemSelected += onMenuItemSelected;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }
        }

        private void onMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.MenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                menuPage.menu.SelectedItem = null;
                IsPresented = false;
            }
        }

        private void ToUserPage_Clicked(object sender, EventArgs e)
        {
            App.ToLoginPageCommand.Execute(null);
        }
    }
}