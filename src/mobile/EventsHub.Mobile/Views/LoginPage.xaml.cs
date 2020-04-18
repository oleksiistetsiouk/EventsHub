using EventsHub.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel { get; set; } = new LoginViewModel(App.NavigationService);

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;

            EmailEntry.Completed += (sender, args) => { PasswordEntry.Focus(); };
            PasswordEntry.Completed += (sender, args) => { ViewModel.AuthenticateCommand.Execute(null); };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}