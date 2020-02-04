using Android.App;
using Android.Widget;
using CLUZMobile.Droid;
using CLUZMobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace CLUZMobile.Droid
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}