using CLUZ.Models;
using CLUZ.Services;
using CLUZ.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CLUZMobile.Helpers
{
    public static class Actions
    {
        public static void LeaveGame()
        {
            PlayersHub.Connection.InvokeAsync("RemovePlayerFromGame", Globals.PlayerObject.Guid, Globals.GameObject.Guid);

        }

        public static void SetAndUpdateState(PlayerState state)
        {
            Globals.PlayerObject.State = state;

            PlayersHub.Connection.InvokeAsync("PlayerStatusUpdate", Globals.PlayerObject.State, Globals.PlayerObject.Guid, Globals.GameObject.Guid);
        }
    }
}
