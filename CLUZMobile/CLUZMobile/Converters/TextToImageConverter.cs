using CLUZ.ViewModels;
using CLUZMobile.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CLUZMobile.Converters
{
    class TextToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && Time.IsDay())
            {
                switch(value.ToString())
                {
                    case "Idle":
                        return ImageSource.FromResource("CLUZMobile.Images.clock.png");
                    case "Ghost":
                        return ImageSource.FromResource("CLUZMobile.Images.ghost.png");
                    case "Kicked":
                        return ImageSource.FromResource("CLUZMobile.Images.door.png");
                    default:
                        return null;
                }
            }
            else if (value != null && !Time.IsDay())
            {
                switch (value.ToString())
                {
                    case "Idle":
                        return ImageSource.FromResource("CLUZMobile.Images.clock_w.png");
                    case "Ghost":
                        return ImageSource.FromResource("CLUZMobile.Images.ghost_w.png");
                    case "Kicked":
                        return ImageSource.FromResource("CLUZMobile.Images.door_w.png");
                    default:
                        return null;
                }
            }
            
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //private bool IsDay()
        //{
        //    int number = Globals.GameObject.TimeFrame;

        //    if (number % 2 == 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
