using System.Collections.Generic;
using Xamarin.Forms;

namespace EventsHub.Mobile.Models
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuData();
            ItemsSource = data;

            BackgroundColor = Color.FromHex("#3C83B8");
            VerticalOptions = LayoutOptions.FillAndExpand;
            SeparatorVisibility = SeparatorVisibility.None;
            ItemTemplate = new DataTemplate(() => 
            {
                var label = new Label { VerticalOptions = LayoutOptions.Center, TextColor = Color.White, Margin = new Thickness(5) };
                label.SetBinding(Label.TextProperty, "Title");

                return new ViewCell { View = label };
            });
        }
    }
}
