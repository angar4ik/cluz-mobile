using CLUZ;
using CLUZ.Models;
using CLUZ.Services;
using CLUZ.ViewModels;
using CLUZ.Views;
using CLUZMobile.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZMobile.ViewModels
{
    class CreateVM : BaseVM
    {
        #region Fields
        private string _gamePinEntryText = "";
        private string _gameNameEntryText = "";
        private double _minimumCount = 4;
        #endregion

        #region Props
        public string GamePinEntryText
        {
            get { return _gamePinEntryText; }
            set
            {
                SetProperty(ref _gamePinEntryText, value);
                CreateCommand.ChangeCanExecute();
            }
        }


        public string GameNameEntryText
        {
            get { return _gameNameEntryText; }
            set
            {
                SetProperty(ref _gameNameEntryText, value);
                CreateCommand.ChangeCanExecute();
            }
        }


        public double MinimumCount
        {
            get { return _minimumCount; }
            set
            {
                SetProperty(ref _minimumCount, value);
            }
        }
        #endregion

        public Command CreateCommand { get; set; }

        public CreateVM()
        {
            CreateCommand = new Command(
            execute: async (o) =>
            {
                await ExecuteCreateGameCommand();
            },
            canExecute: (o) =>
            {
                string gameName = GameNameEntryText.Trim();
                string gamePin = GamePinEntryText.Trim();

                if (!String.IsNullOrEmpty(gameName) && !String.IsNullOrEmpty(gamePin))
                    return true;
                else
                    return false;
            });
        }

        private async Task ExecuteCreateGameCommand()
        {
            string gameName = GameNameEntryText.Trim();
            string gamePin = GamePinEntryText.Trim();

            Console.WriteLine("ExecuteCreateGameCommand fired");

            bool result = await PlayersHub.Connection.InvokeAsync<bool>("GameNameExistsInPool", gameName);

            if (!result)
            {
                Guid newGameGuid = await PlayersHub.Connection.InvokeAsync<Guid>("CreateGame", gameName, gamePin, MinimumCount);

                DependencyService.Get<IMessage>().ShortAlert($"Game {gameName} have been created");

                GameNameEntryText = "";

                await JoinGame(newGameGuid, gamePin);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert($"Game {gameName} existing on server");
            }
        }

        private async Task JoinGame(Guid gameGuid, string gamePin)
        {
            Game game = await PlayersHub.Connection.InvokeAsync<Game>("GetGameByGuid", gameGuid);

            string pinSha256 = ComputeSha256Hash(gamePin);

            if (pinSha256 == game.GamePin)
            {
                if (game.Status != GameState.Locked)
                {
                    App.Current.MainPage = new GamePage(gameGuid);
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert($"Game {game.Name} is locked");
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert($"Pin for game {game.Name} is wrong");
            }

            string ComputeSha256Hash(string rawData)
            {
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    return builder.ToString();
                }
            }
        }
    }
}
