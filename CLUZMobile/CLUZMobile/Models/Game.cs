using System;

namespace CLUZ.Models
{
    public class Game
    {
        public GameState Status { get; set; }

        public string Name { get; set; }

        public string GamePin { get; set; }

        public Guid Guid { get; set; }

        public int TimeFrame { get; set; }

        public int MinimumPlayerCount { get; set; }
    }
}
