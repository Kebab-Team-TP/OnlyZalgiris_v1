using System;
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

                /*
                 * Determining whether a new user is admin
                 * Admin has to enter characters *&! in the beginning
                 * if he wants to have admin privileges
                */
                char[] usernameChars = username.ToCharArray();
                if (usernameChars[0] == '*' && usernameChars[1] == '&' && usernameChars[2] == '!')
                    newUser.IsAdmin = true;
                else
                    newUser.IsAdmin = false;
                //--------------------------------

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