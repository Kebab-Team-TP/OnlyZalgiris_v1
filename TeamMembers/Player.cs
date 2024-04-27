using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace Zalgiris.TeamMembers
{
    public class Player : Member
    {
        public string Number { get; set; }
        public string Points { get; set; }


        public Player(string number, string name, string position, string imageLink): base(name, position, imageLink)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number + " " + Name + " " + Position;
        }
        public static string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
    }
}