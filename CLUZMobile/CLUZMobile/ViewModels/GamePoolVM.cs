using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Text;
using CLUZ.Models;
using CLUZ.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Cryptography;
using CLUZ.Views;
using CLUZMobile.Interfaces;
using CLUZMobile;

namespace CLUZ.ViewModels
{
    
    public class GamePoolVM : BaseVM
    {
        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set { SetProperty(ref _playerName, value); }
        }

        private string _gamePinEntryText = "";
        public string GamePinEntryText
        {
            get { return _gamePinEntryText; }
            set
            {
                SetProperty(ref _gamePinEntryText, value);
                CreateCommand.ChangeCanExecute();
                JoinCommand.ChangeCanExecute();
            }
        }

        private string _gameNameEntryText = "";
        public string GameNameEntryText
        {
            get { return _gameNameEntryText; }
            set
            {
                SetProperty(ref _gameNameEntryText, value);
                CreateCommand.ChangeCanExecute();
            }
        }

        private double _minimumCount = 4;
        public double MinimumCount
        {
            get { return _minimumCount; }
            set
            {
                SetProperty(ref _minimumCount, value);
            }
        }

        Game _selectedItem;
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
        public Command CreateCommand { get; set; }
        public Command JoinCommand { get; set; }
        public GamePoolVM()
        {
            Items = new ObservableCollection<Game>();

            LoadItemsCommand = new Command(async () => await LoadGames());

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

            PlayerName = Globals.PlayerObject.Name;
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

        private async Task ExecuteCreateGameCommand()
        {
            string gameName = GameNameEntryText.Trim();
            string gamePin = GamePinEntryText.Trim();

            Console.WriteLine("ExecuteCreateGameCommand fired");

            bool result = await PlayersHub.Connection.InvokeAsync<bool>("GameNameExistsInPool", gameName);  

            if (!result)
            {
                //await PlayersHub.Connection.InvokeAsync<Guid>("CreateGame", gameName, gamePin, MinimumCount);

                DependencyService.Get<IMessage>().ShortAlert($"Game {gameName} have been created");

                GameNameEntryText = "";
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert($"Game {gameName} existing on server");
            }
        }

        private async Task ExecuteJoinGameCommand()
        {
            //TODO: disable button after first click
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
