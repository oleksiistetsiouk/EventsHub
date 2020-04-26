using EventsHub.Mobile.Views;
using System.Collections.Generic;

namespace EventsHub.Mobile.Models
{
    public class MenuData : List<MenuItem>
    {
        public MenuData()
        {
            this.Add(new MenuItem()
            {
                Title = "Browse",
                TargetType = typeof(ItemsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "About",
                TargetType = typeof(AboutPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Films",
                TargetType = typeof(FilmsPage)
            });
        }
    }
}
