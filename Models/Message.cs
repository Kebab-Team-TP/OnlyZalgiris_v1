using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zalgiris.Models
{
    public class Message
    {

        public string text { get; set; }
        public string datetime { get; set; }
        public string author { get; set; }
        public Message()
        {
            text = "Empty Text";

        }
        //public DateTime time;
        //public User User;
        public Message(string text, string datetime, string author)//, DateTime time=null, User user=null)
        {
            this.text = text;
            this.datetime = datetime;
            this.author = author;
        }
    }
}