using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using CLUZ.Services;
using CLUZ.Views;
using CLUZMobile.Interfaces;
using CLUZ.Models;
using CLUZMobile;

namespace CLUZ.ViewModels
{

    public class RegisterViewModel : BaseViewModel
    {

        public Command RegisterCommand { set; get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(
            execute: async (EntryText) =>
            {
                string text = EntryText as string;

                await ExecuteRegisterCommand(text);
            },
            canExecute: (EntryText) =>
            {
                string text = EntryText as string;
                if (!String.IsNullOrEmpty(text))
                    return true;
                else
                    return false;
            });
        }

        async Task ExecuteRegisterCommand(string EntryText)
        {
            ActivitySpin = true;

            await PlayersHub.Connect();

            bool result = await PlayersHub.Connection.InvokeAsync<bool>("PlayerNameExistsInPool", EntryText);

            if(!result)
            {
                Guid playerGuid = await PlayersHub.Connection.InvokeAsync<Guid>("InitPlayer", EntryText);

                Globals.PlayerObject.Guid = playerGuid;

                Globals.PlayerObject.Name = EntryText.Trim();

                ActivitySpin = false;

                App.Current.MainPage = new GamePoolPage();

            }
            else
            {
                ActivitySpin = false;

                DependencyService.Get<IMessage>().ShortAlert("Name existing");
            }
        }
    }
}
