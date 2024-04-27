using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Zalgiris.Models;
using Zalgiris.App;

namespace Zalgiris
{
    public partial class Login : System.Web.UI.Page
    {
        static string relativePath = "App_Data/Users.json";
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string fullPath = Path.Combine(currentDirectory, relativePath);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                AuthenticateUser();
            }
        }

        private void AuthenticateUser()
        {
            // Assuming the form uses method="post" and the input fields have name="username" and name="password"
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            // Read the user data from the file
            string json = File.ReadAllText(fullPath);
            List<User> users = UsersController.GetAll(); // Assuming this method deserializes the JSON to a List<User>

            // Authenticate the user
            User authenticatedUser = users.FirstOrDefault(user => user.Username == username && user.Password == password);

            if (authenticatedUser != null)
            {
                // Set authenticated user's session variables
                Session["UserName"] = authenticatedUser.Username;
                Session["authenticated"] = true;

                // Check if the authenticated user is an admin
                if (authenticatedUser.IsAdmin)
                {
                    Session["IsAdmin"] = true; // Set IsAdmin session variable to true for admins
                    Session["authorized"] = true;
                    Response.Redirect("~/Admin.aspx");
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            else
            {
                // Optionally, display a login failed message.
                ExceptionLabel.Visible = true;
                ExceptionLabel.Text = "Login failed. Try again.";
            }
        }

        protected void LinkRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}
