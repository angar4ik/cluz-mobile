using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;
using CLUZ.Models;
using CLUZ.Services;
using CLUZMobile.Interfaces;
using CLUZMobile;
using CLUZMobile.Helpers;
using CLUZ.Views;
using System.Linq;

//App.Current.MainPage.Navigation.PushModalAsync(new CountDownPage(), true);

namespace CLUZ.ViewModels
{
    public class GameVM : BaseVM
    {
        #region SelectedItem
        private Player _selectedItem;
        public Player SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        #endregion

        #region MultiButtonText
        private string _multiButtonText = "Ready";
        public string MultiButtonText
        {
            get { return _multiButtonText; }
            set { SetProperty(ref _multiButtonText, value); }
        }
        #endregion

        #region BackgroundImageSource
        private ImageSource _backgroundImageSource = ImageSource.FromResource("CLUZMobile.Images.city.jpg");
        public ImageSource BackgroundImageSource
        {
            get { return _backgroundImageSource; }
            set { SetProperty(ref _backgroundImageSource, value); }
        }
        #endregion

        #region BackgroundColor
        private Color _backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }
        #endregion

        #region TextColor
        private Color _textColor = Color.Black;
        public Color TextColor
        {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }
        #endregion

        #region MyRoleLabelText
        private string _myRoleLabelText;
        public string MyRoleLabelText
        {
            get { return _myRoleLabelText; }
            set { SetProperty(ref _myRoleLabelText, value); }
        }
        #endregion

        public int timeFrameCount = 0;

        bool _multiCommandExecuted = false;

        bool _didIVoted = false;

        //bool _didIWaited = false;

        //bool _areWeWaitingTimer = false;

        bool _modalOpened = false;

        public Command MultiCommand { get; set; }
        public ObservableCollection<Player> Items { get; set; } = new ObservableCollection<Player>();
        public GameVM(Guid gameGuid)
        {
            Globals.GameObject.Guid = gameGuid;

            #region MultiCommand
            MultiCommand = new Command(
            execute: (o) =>
            {
                ExecuteMultiCommand(MultiButtonText);
            },
            canExecute: (o) =>
            {
                if (/*Globals.GameObject.Status == GameState.Unfilled*/
                /*||*/ Globals.PlayerObject.Role == PlayerRole.Ghost
                || Globals.PlayerObject.Role == PlayerRole.Kicked
                || _multiCommandExecuted == true)
                    return false;
                else
                    return true;
            });
            #endregion

            #region Server Call Handlers
            PlayersHub.Connection.On<Game, Guid>("GameChanged", (game, guid) =>
            {
                if (guid == Globals.GameObject.Guid)
                {
                    RefreshGameObject(game);
                    MultiCommand.ChangeCanExecute();
                }  
            });

            PlayersHub.Connection.On<Player, Guid>("PlayerChanged", (player, guid) =>
            {
                if (guid == Globals.GameObject.Guid)
                {
                    RefreshPlayer(player);
                    MultiCommand.ChangeCanExecute();
                }
            });

            PlayersHub.Connection.On<List<Player>, Guid>("PlayerListChanged", (players, guid) =>
            {
                if (guid == Globals.GameObject.Guid)
                {
                    RefreshListPlayers(players);
                    MultiCommand.ChangeCanExecute();
                }
            });

            PlayersHub.Connection.On<int, string, bool, Guid>("ShowModal", async (time, text, endGame, guid) =>
            {
                if (guid == Globals.GameObject.Guid)
                {
                    if (_modalOpened == false)
                    {
                        _modalOpened = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new CountDownPage(time, text, endGame), true);
                        _modalOpened = false;
                    }
                }
            });
            #endregion

            AddPlayerToGame(gameGuid);
        }
        public async void AddPlayerToGame(Guid gameGuid)
        {
            await PlayersHub.Connection.InvokeAsync("AddPlayerToGame", Globals.PlayerObject.Guid, gameGuid);
        }
        private void RefreshPlayer(Player player)
        {
            int idx = -1;

            foreach (Player p in Items)
            {
                if(p.Guid == player.Guid)
                {
                    idx = Items.IndexOf(p);
                }
            }

            if(idx != -1)
            {
                Items.RemoveAt(idx);
                Items.Insert(idx, player);
            }


            if (player.Guid == Globals.PlayerObject.Guid)
            {
                Globals.PlayerObject = player;

                MyRoleLabelText = $"Role: {Globals.PlayerObject.Role.ToString()}";

                UpdateMultiButtonText();

                //Console.WriteLine($"Local player {Globals.PlayerObject.Guid} replaced by {player.Guid}");
            }  
        }
        public void RefreshGameObject(Game game)
        {
            try
            {
                Globals.GameObject = game;

                UpdateGame();
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert($"Caught exception at RefreshGameObject()");
            }
        }
        public void RefreshListPlayers(List<Player> players)
        {
            Guid tempSelectedGuid = Guid.Empty;

            if (SelectedItem != null)
            {
                tempSelectedGuid = SelectedItem.Guid;
            }

            ActivitySpin = true;

            try
            {
                Items.Clear();

                players.ForEach(p =>
                {
                    Items.Add(p);

                    // updateing local P object
                    if (p.Guid == Globals.PlayerObject.Guid)
                    {
                        Globals.PlayerObject = p;
                    }

                    // Resotring selected item
                    if (tempSelectedGuid != Guid.Empty
                    && p.Guid == tempSelectedGuid
                    && p.Role != PlayerRole.Ghost
                    && p.Role != PlayerRole.Kicked)
                    {
                        SelectedItem = p;
                    }
                });
            }
            catch
            {
                DependencyService.Get<IMessage>().LongAlert($"Caught exception at InitListOfPlayers()");
            }

            ActivitySpin = false;
        }
        private void ExecuteMultiCommand(string buttonText)
        {
            _multiCommandExecuted = true;

            if (buttonText.Contains("Ready"))
            {
                Actions.SetAndUpdateState(PlayerState.Ready);
            }

            else if (buttonText.Contains("Kill") && SelectedItem != null)
            {
                PlayersHub.Connection.InvokeAsync("KillRequest", Globals.PlayerObject.Guid, SelectedItem.Guid, Globals.GameObject.Guid);

                Actions.SetAndUpdateState(PlayerState.Ready);
            }

            else if (buttonText.Contains("Guess") && SelectedItem != null)
            {
                if (SelectedItem.Role == PlayerRole.Mafia)
                {
                    DependencyService.Get<IMessage>().LongAlert($"'{SelectedItem.Name}' IS the Mafia!");
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert($"'{SelectedItem.Name}' NOT a Mafia");
                }

                Actions.SetAndUpdateState(PlayerState.Ready);
            }

            else if (buttonText.Contains("Vote") && SelectedItem != null)
            {
                PlayersHub.Connection.InvokeAsync("VoteRequest", Globals.PlayerObject.Guid, SelectedItem.Guid, Globals.GameObject.Guid);

                _multiCommandExecuted = false;
                _didIVoted = true;
                MultiButtonText = "Ready";
            }

            else
            {
                _multiCommandExecuted = false;
                DependencyService.Get<IMessage>().ShortAlert("Select player");
            }

            MultiCommand.ChangeCanExecute();

            UpdateMultiButtonText();

        }
        private void UpdateGame()
        {
            // resetting Ready button in case of unfilled game
            if (Globals.GameObject.Status == GameState.Unfilled)
                _multiCommandExecuted = false;

            // reseting for next day
            if (timeFrameCount != Globals.GameObject.TimeFrame)
            {
                _multiCommandExecuted = false;
                _didIVoted = false;
            }

            // setting colors only when after day changes
            Theme.SetTheme(this);
        }

        public void UpdateMultiButtonText()
        {
            if (Globals.PlayerObject.Role == PlayerRole.Mafia
                && !Time.IsDay())
            {
                MultiButtonText = "Kill";
            }
            else if (Globals.PlayerObject.Role == PlayerRole.Police
                && !Time.IsDay())
            {
                MultiButtonText = "Guess";
            }
            else if (Globals.GameObject.TimeFrame >= 2
                && Time.IsDay() && _didIVoted == false)
            {
                MultiButtonText = "Vote";
            }
            else if (Globals.PlayerObject.Role == PlayerRole.Citizen
                && !Time.IsDay())
            {
                MultiButtonText = "Ready";
            }
        }
    }
}