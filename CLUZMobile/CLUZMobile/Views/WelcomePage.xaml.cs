using CLUZ;
using CLUZ.Services;
using CLUZ.Views;
using CLUZMobile.Interfaces;
using CLUZMobile.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CLUZMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();

            this.BindingContext = new WelcomeVM();
        }

        protected override bool OnBackButtonPressed()
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();

            DependencyService.Get<IMessage>().ShortAlert("Logged out from server");

            PlayersHub.Connection.InvokeAsync("RemovePlayerFromPool", Globals.PlayerObject.Guid);

            _ = PlayersHub.Disonnect();

            App.Current.MainPage = new RegisterPage();

            return true;
        }
    }
}