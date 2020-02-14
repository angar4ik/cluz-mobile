using CLUZ.Views;
using CLUZMobile.Helpers;
using CLUZMobile.ViewModels;
using CLUZMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZ.ViewModels
{
    class CountDownVM : BaseVM
    {
        #region Text
        private string _text = "Empty";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        #endregion

        public CountDownVM(int time, string text, bool endGame)
        {
            int timer = time;

            Text = text;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                timer--;

                if (timer > 0)
                {
                    return true;
                }
                else
                {
                    if (endGame)
                    {
                        App.Current.MainPage.Navigation.PopModalAsync();
                        
                        App.Current.MainPage = new WelcomePage();

                        return false;
                    }
                    else
                    {
                        App.Current.MainPage.Navigation.PopModalAsync();

                        return false;
                    }
                }

            });
        }
    }
}
