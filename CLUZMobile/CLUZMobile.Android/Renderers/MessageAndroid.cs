using Android.App;
using Android.Support.Design.Widget;
using Android.Widget;
using CLUZMobile.Droid;
using CLUZMobile.Interfaces;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace CLUZMobile.Droid
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            //Toast.MakeText(Application.Context, message, ToastLength.Long).Show();

            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(view, message, 5000).Show();
        }

        public void ShortAlert(string message)
        {
            //Toast.MakeText(Application.Context, message, ToastLength.Short).Show();

            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(view, message, 2000).Show();
        }

        public void CustomAlert(string message, int howLong)
        {
            //Toast.MakeText(Application.Context, message, ToastLength.Short).Show();

            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(view, message, howLong * 1000).Show();
        }
    }
}