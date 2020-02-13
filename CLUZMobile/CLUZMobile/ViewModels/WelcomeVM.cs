using CLUZ;
using CLUZ.ViewModels;
using CLUZMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZMobile.ViewModels
{
    class WelcomeVM : BaseVM
    {
        public Command CreateCommand { get; set; }
        public Command JoinCommand { get; set; }
        public WelcomeVM()
        {
            CreateCommand = new Command(
            execute: (o) =>
            {
                ExecuteCreateGameCommand();
            },
            canExecute: (o) =>
            {
                //string gameName = GameNameEntryText.Trim();
                //string gamePin = GamePinEntryText.Trim();

                //if (!String.IsNullOrEmpty(gameName) && !String.IsNullOrEmpty(gamePin))
                //    return true;
                //else
                //    return false;
                return true;
            });

            JoinCommand = new Command(
            execute: (o) =>
            {
                ExecuteJoinGameCommand();
            },
            canExecute: (o) =>
            {
                //string gamePin = GamePinEntryText.Trim();

                //if (!String.IsNullOrEmpty(gamePin) && SelectedItem != null)
                //    return true;
                //else
                //    return false;

                return true;
            });
        }

        private void ExecuteCreateGameCommand()
        {
            App.Current.MainPage = new CreatePage();
        }

        private void ExecuteJoinGameCommand()
        {
            App.Current.MainPage = new JoinPage();
        }


    }
}
