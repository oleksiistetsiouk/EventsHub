using EventsHub.Mobile.Models;
using EventsHub.Mobile.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventsHub.Mobile.ViewModels
{
    public class TheatrePlayViewModel : INotifyPropertyChanged
    {
        private readonly TheatreService theatreService = new TheatreService();
        private List<TheatrePlay> theatrePlays;

        public List<TheatrePlay> TheatrePlays
        {
            get { return theatrePlays; }
            set
            {
                theatrePlays = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetTheatrePlaysCommand
        {
            get
            {
                return new Command(async () =>
                {
                    TheatrePlays = (await theatreService.GetAllTheatrePlays()).ToList();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
