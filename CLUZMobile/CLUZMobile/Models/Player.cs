using CLUZ.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLUZ.Models
{
    public class Player
    {
        public string ConnId { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public PlayerState State { get; set; }

        public PlayerRole Role { get; set; }

        public int VoteCount { get; set; }

        public bool AllowedToVote { get; set; }
    }
}
