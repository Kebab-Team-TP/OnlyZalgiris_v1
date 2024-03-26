using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zalgiris.TeamMembers
{
    public class Player
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; } // For both players and staff
        public string ImageLink { get; set; }

        public Player(string number, string name, string position, string imageLink)
        {
            Number = number;
            Name = name;
            Position = position;
            ImageLink = imageLink;
        }

        public override string ToString()
        {
            return Number + " " + Name + " " + Position;
        }
    }
}