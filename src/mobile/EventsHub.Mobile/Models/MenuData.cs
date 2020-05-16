using EventsHub.Mobile.Views;
using System.Collections.Generic;

namespace EventsHub.Mobile.Models
{
    public class MenuData : List<MenuItem>
    {
        public MenuData()
        {
            Add(new MenuItem()
            {
                Title = "Films",
                TargetType = typeof(FilmsPage)
            });
            Add(new MenuItem()
            {
                Title = "Theatre Plays",
                TargetType = typeof(TheatrePlaysPage)
            });
            Add(new MenuItem()
            {
                Title = "Concerts",
                TargetType = typeof(ConcertsPage)
            });
            Add(new MenuItem()
            {
                Title = "About",
                TargetType = typeof(AboutPage)
            });
        }
    }
}
