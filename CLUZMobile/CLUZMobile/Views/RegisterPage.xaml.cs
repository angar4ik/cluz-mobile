using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;
using CLUZ.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace CLUZ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.BindingContext = new RegisterVM();

            //ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            //if (mergedDictionaries != null)
            //{
            //    mergedDictionaries.Clear();
            //    mergedDictionaries.Add(new DarkTheme());
            //}
        }

        protected override bool OnBackButtonPressed()
        {

            PlayersHub.Connection.InvokeAsync("RemovePlayerFromPool");

            return false;
        }
    }

    
}