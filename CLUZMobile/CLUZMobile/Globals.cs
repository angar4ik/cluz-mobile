using CLUZ.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLUZMobile
{
    public static class Globals
    {
        //static string hostName = "veratoliquehouse.duckdns.org";
        static string hostName = "localhost";
        static string hostPort = "8000";

        public static string Host { get; } = $"{hostName}:{hostPort}";
        public static Player PlayerObject { get; set; } = new Player();
        public static Game GameObject { get; set; } = new Game();
    }
}
