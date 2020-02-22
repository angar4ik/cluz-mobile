using CLUZ.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CLUZMobile.Converters
{
    class RoleToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Player p = (Player)value;

                if (p.Role == PlayerRole.Ghost || p.Role == PlayerRole.Kicked)
                {
                    return 0.20;
                }
            }

            return 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
