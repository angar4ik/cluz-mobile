using CLUZ.ViewModels;
using CLUZMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZMobile.Helpers
{
    public static class Time
    {
        public static bool IsDay()
        {
            int number = Globals.GameObject.TimeFrame;

            if (number % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task CountDownTimerAsync(GameVM model)
        {
            for(int i = 30; i > 1; i--)
            {
                //DependencyService.Get<IMessage>().ShortAlert(i.ToString());
                model.MultiButtonText = i.ToString();
                await Task.Delay(1000);
            }

            //
        }
    }
}
