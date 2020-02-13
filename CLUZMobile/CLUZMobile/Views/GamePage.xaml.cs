using Microsoft.AspNetCore.SignalR.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;
using CLUZ.Services;
using CLUZ.Models;
using System.Threading.Tasks;
using CLUZMobile;
using CLUZMobile.Helpers;
using CLUZMobile.Views;

namespace CLUZ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        GameVM viewModel;
        public GamePage(Guid gameGuid)
        {
            InitializeComponent();

            this.BindingContext = viewModel = new GameVM(gameGuid);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            if(Globals.GameObject.Status == GameState.Locked)
            {
                DisplayAlert("Leave?", "You're probably ruining the game. Sure?", "Yes", "No").ContinueWith(async t =>
                {
                    if (t.Result)
                    {
                        Actions.LeaveGame();

                        App.Current.MainPage = new WelcomePage();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                Actions.LeaveGame();

                App.Current.MainPage = new WelcomePage();
            }

            return true;
        }
    }
}
