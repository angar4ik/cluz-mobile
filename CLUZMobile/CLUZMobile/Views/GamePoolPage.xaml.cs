using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;
using CLUZ.Services;
using Microsoft.AspNetCore.SignalR.Client;
using CLUZMobile;
using CLUZMobile.Interfaces;

namespace CLUZ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePoolPage : ContentPage
    {
        GamePoolVM viewModel;
        public GamePoolPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new GamePoolVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
                
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();

            DependencyService.Get<IMessage>().ShortAlert("Logged out from server");

            PlayersHub.Connection.InvokeAsync("RemovePlayerFromPool", Globals.PlayerObject.Guid);

            PlayersHub.Disonnect();

            App.Current.MainPage = new RegisterPage();

            return true;
        }
    }
}
