using Xamarin.Forms;
using CLUZ.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace CLUZ
{
    public partial class App : Application
    {
        public App()
        {
            AppCenter.Start("android=598860f1-f3c1-48c1-8d53-5a51cd011df2;",
                  typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            MainPage = new RegisterPage(); 
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
