using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zalgiris.TeamMembers
{
    public class Staff
    {
        public string Name { get; set; }
        public string Role { get; set; } // Staff role
        public string ImageLink { get; set; }

        public Staff(string name, string role, string imageLink)
        {
            Name = name;
            Role = role;
            ImageLink = imageLink;
        }

        public override string ToString()
        {
            return Name + " " + Role;
        }
    }
}