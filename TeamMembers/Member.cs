using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace Zalgiris.TeamMembers
{

    public abstract class Member
    {
        public string Name { get; set; }
        public string Position { get; set; } // For both players and staff
        public string ImageLink { get; set; }
        public string Slug { get; set; }
        public string Height { get; set; }
        public string Age { get; set; }
        public string Info { get; set; }
        public string Weigth { get; set; }
        public Member(string name, string position, string imageLink)
        {
            Name = name;
            Position = position;
            ImageLink = imageLink;
            Slug = ToUrlSlug(name);
            Height = "";
            Age = "";
            Weigth = "";
            Info = "";
        }

        public override string ToString()
        {
            return Name + " " + Position;
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