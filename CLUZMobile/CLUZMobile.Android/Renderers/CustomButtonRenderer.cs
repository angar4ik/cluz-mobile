using Android.Content;
using Android.Support.V7.View;
using Android.Support.V7.Widget;
using CLUZMobile.Droid;
using CLUZMobile.CustomRenderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace CLUZMobile.Droid
{
    public class CustomButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public CustomButtonRenderer(Context context) : base(context)
        {
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //    if (e.PropertyName == Button. IsEnabledProperty.PropertyName)
        //    {
        //        this.ModifyTextColor();
        //    }
        //}

        ////protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        ////{
        ////    base.OnElementChanged(e);

        ////    if (e.OldElement != null)
        ////    {
        ////        // Cleanup
        ////    }

        ////    if (e.NewElement != null)
        ////    {
        ////        this.ModifyTextColor();
        ////    }
        ////}

        //private void ModifyTextColor()
        //{
        //    if (this.Element. IsEnabled == false)
        //    {
        //        this.Control.SetTextColor(Android.Graphics.Color.Red);
        //    }
        //}

        //protected override AppCompatButton CreateNativeControl()
        //{
        //    var context = new ContextThemeWrapper(Context, Resource.Style.Widget_AppCompat_Button_Borderless);
        //    var button = new AppCompatButton(context, null, Resource.Style.Widget_AppCompat_Button_Borderless);
        //    return button;
        //}
        //protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.OldElement != null)
        //    {
        //        // Cleanup
        //    }

        //    if (e.NewElement != null)
        //    {
        //        //Control.SetShadowLayer(5, 3, 3, Color.Black.ToAndroid());
        //        //Control.SetBackgroundColor(Color.Black.ToAndroid());
        //        Control.SetTextColor(Color.Red.ToAndroid());
        //    }
        //}
    }
}