using CLUZ.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CLUZMobile.Helpers
{
    public static class Theme
    {
        public static void SetTheme(GameVM gameVeiwModel)
        {
            if (Time.IsDay())
            {
                gameVeiwModel.BackgroundColor = Color.White;
                gameVeiwModel.TextColor = Color.Black;
                gameVeiwModel.BackgroundImageSource = ImageSource.FromResource("CLUZMobile.Images.city.jpg");
            }
            else if (!Time.IsDay())
            {
                gameVeiwModel.BackgroundColor = Color.Black;
                gameVeiwModel.TextColor = Color.White;
                gameVeiwModel.BackgroundImageSource = ImageSource.FromResource("CLUZMobile.Images.sky.jpg");
                
            }
        }
    }
}
