using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using CLUZ.Services;

namespace CLUZ.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Icon = "@mipmap/ic_launcher", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected async override void OnResume()
        {
            base.OnResume();

            await PlayersHub.Connect();

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

    }
}