using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.Json;
using System.IO;
using Antlr.Runtime.Tree;
using Microsoft.AspNet.FriendlyUrls;
using Zalgiris.App;
using Zalgiris.Models;

namespace Zalgiris
{
    

    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string email = Request.Form["email"];
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
            {
                User newUser = new User();
                newUser.Username = username;
                newUser.Password = password;
                newUser.Email = email;
                newUser.IsAdmin = false;
                try
                {
                    UsersController.Add(newUser);

                }
                catch( Exception ex)
                {
                    ExceptionLabel.Text = ex.Message;
                    return;
                }
                ExceptionLabel.Text = "Registracija pavyko";

            }
            else
            {
                ExceptionLabel.Text = "Nepalikite tuščių laukų";
            }

        }

    }
}