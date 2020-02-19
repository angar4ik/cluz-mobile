using CLUZ;
using CLUZ.Models;
using CLUZ.Services;
using CLUZ.ViewModels;
using CLUZ.Views;
using CLUZMobile.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CLUZMobile.ViewModels
{
    class JoinVM : BaseVM
    {
        #region Fields
        private string _gamePinEntryText = "";
        Game _selectedItem;
        #endregion

        #region Props
        public string GamePinEntryText
        {
            get { return _gamePinEntryText; }
            set
            {
                SetProperty(ref _gamePinEntryText, value);
                JoinCommand.ChangeCanExecute();
            }
        }
        public Game SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                JoinCommand.ChangeCanExecute();
            }
        }
        public ObservableCollection<Game> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command JoinCommand { get; set; }
        #endregion

        public JoinVM()
        {
            Items = new ObservableCollection<Game>();

            LoadItemsCommand = new Command(async () => await LoadGames());

            JoinCommand = new Command(
            execute: async (o) =>
            {
                await ExecuteJoinGameCommand();
            },
            canExecute: (o) =>
            {
                string gamePin = GamePinEntryText.Trim();

                if (!String.IsNullOrEmpty(gamePin) && SelectedItem != null)
                    return true;
                else
                    return false;
            });

            PlayersHub.Connection.On("RefreshGameList", async () =>
            {
                await LoadGames();
            });
        }

        public async Task LoadGames()
        {
            ActivitySpin = true;

            List<Game> _games = await PlayersHub.Connection.InvokeAsync<List<Game>>("GetAllGames");

            Items.Clear();

            foreach (Game item in _games)
            {
                Items.Add(item);
            }

            ActivitySpin = false;
        }
        private async Task ExecuteJoinGameCommand()
        {
            string gamePin = GamePinEntryText.Trim();

            await JoinGame(SelectedItem.Guid, gamePin);
        }

        private async Task JoinGame(Guid gameGuid, string gamePin)
        {
            //await GamesHub.Connect();

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
