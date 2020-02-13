using CLUZ;
using CLUZ.Services;
using CLUZ.ViewModels;
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
    public partial class JoinPage : ContentPage
    {
        JoinVM viewModel;

        public JoinPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new JoinVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new WelcomePage();

            return true;
        }
    }
}