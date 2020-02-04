using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;
using CLUZ.Services;

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
            System.Diagnostics.Process.GetCurrentProcess().Kill();

            return true;
        }
    }
}
