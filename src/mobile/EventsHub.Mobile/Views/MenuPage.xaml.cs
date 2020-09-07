using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace EventsHub.Mobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        public ListView menu;

        public MenuPage()
        {
            InitializeComponent();
            Title = AppConstants.APP_NAME;
            BackgroundColor = Color.FromHex("#3C83B8");
            Padding = new Thickness(10);
            menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 15, 0, 15),
                Content = new Label
                {
                    TextColor = Color.White,

                    Text = AppConstants.APP_NAME,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 18
                }
            };

            var separator = new BoxView
            {
                Margin = new Thickness(0, 0, 0, 10),
                HeightRequest = 0.5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Color = Color.White
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            layout.Children.Add(menuLabel);
            layout.Children.Add(separator);
            layout.Children.Add(menu);

            Content = layout;
        }
    }
}