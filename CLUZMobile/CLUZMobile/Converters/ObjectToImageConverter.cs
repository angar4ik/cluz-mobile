using CLUZ.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CLUZMobile.Converters
{
    class ObjectToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Player p = (Player)value;

                switch (p.Role)
                {
                    case PlayerRole.Ghost:
                        return ImageSource.FromResource("CLUZMobile.Images.speechless-b.png");
                    case PlayerRole.Kicked:
                        return ImageSource.FromResource("CLUZMobile.Images.door-b.png");
                }

                if (p.AllowedToVote && !p.HasVoted)
                {
                    return ImageSource.FromResource("CLUZMobile.Images.vote-b.png");
                }
                else if(p.AllowedToVote && p.HasVoted)
                {
                    return ImageSource.FromResource("CLUZMobile.Images.thumb-down-b.png");
                }
                else if(p.State == PlayerState.Idle)
                {
                    return ImageSource.FromResource("CLUZMobile.Images.waiting-b.png");
                }

                return null;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
