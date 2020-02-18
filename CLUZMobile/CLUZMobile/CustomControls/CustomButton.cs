using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CLUZMobile.CustomControls
{
    class CustomButton : Button
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "IsEnabled" && this.IsEnabled == true)
            {
                AnimateAsync();
            }

            base.OnPropertyChanged(propertyName);
        }

        private async void AnimateAsync()
        {
            await this.TranslateTo(-15, 0, 50);
            await this.TranslateTo(15, 0, 50);
            await this.TranslateTo(-15, 0, 50);
            await this.TranslateTo(15, 0, 50);
            await this.TranslateTo(-15, 0, 50);
            await this.TranslateTo(15, 0, 50);
            await this.TranslateTo(0, 0, 50);
        }
    }
}
