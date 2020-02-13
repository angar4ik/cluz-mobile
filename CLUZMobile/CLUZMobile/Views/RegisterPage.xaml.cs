using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CLUZ.ViewModels;

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
            //System.Diagnostics.Process.GetCurrentProcess().Kill();

            //PlayersHub.Disonnect();

            return false;
        }
    }

    
}