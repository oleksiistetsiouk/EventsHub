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
                Title = "Кіно",
                TargetType = typeof(FilmsPage)
            });
            Add(new MenuItem()
            {
                Title = "Театр",
                TargetType = typeof(TheatrePlaysPage)
            });
            Add(new MenuItem()
            {
                Title = "Концерти",
                TargetType = typeof(ConcertsPage)
            });
            Add(new MenuItem()
            {
                Title = "Про додаток",
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
