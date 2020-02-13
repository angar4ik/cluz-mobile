using CLUZ;
using CLUZ.Services;
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
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();

            this.BindingContext = new CreateVM();
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new WelcomePage();

            return true;
        }
    }
}