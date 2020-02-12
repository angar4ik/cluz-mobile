using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;
using CLUZ.Services;
using Microsoft.AspNetCore.SignalR.Client;
using CLUZMobile;

namespace CLUZ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePoolPage : ContentPage
    {
        GamePoolViewModel viewModel;
        public GamePoolPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new GamePoolViewModel();
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

            PlayersHub.Connection.InvokeAsync("RemovePlayerFromPool", Globals.PlayerObject.Guid);

            PlayersHub.Disonnect();

            App.Current.MainPage = new RegisterPage();

            return true;
        }
    }
}
