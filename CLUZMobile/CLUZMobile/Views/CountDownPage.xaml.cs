using CLUZ.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CLUZ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountDownPage : ContentPage
    {
        CountDownViewModel viewModel;
        public CountDownPage(int time, string text, bool endGame)
        {
            InitializeComponent();

            this.BindingContext = viewModel = new CountDownViewModel(time, text, endGame);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}