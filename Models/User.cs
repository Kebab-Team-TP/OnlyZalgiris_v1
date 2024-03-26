using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.Json;
namespace Zalgiris.Models
{
    public class User
    {


        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        
        
    }
}