using Android.Content;
using Android.Views;
using CLUZMobile.Droid;
using System.ComponentModel;
using CLUZMobile.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace CLUZMobile.Droid
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cell;
        private bool _isSelected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cell = base.GetCellCore(item, convertView, parent, context);

            _isSelected = false;

            _cell.SetBackgroundColor(Android.Graphics.Color.Transparent);

            return _cell;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if (e.PropertyName == "IsSelected")
            {
                _isSelected = !_isSelected;

                if (_isSelected)
                {
                    _cell.SetBackgroundColor(Android.Graphics.Color.LightGray);
                }
                else
                {
                    _cell.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }
    }

}
