using Microsoft.Ajax.Utilities;
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
using Zalgiris.Models;
using Zalgiris.App;
using System.Web.Services.Description;

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
            bool isAuthenticated = users.Any(user => user.Username == username && user.Password == password);

            if (isAuthenticated)
            {
                Session["UserName"] = username;
                Session["authenticated"] = true;
                Response.Redirect("/"); // Redirect to home page on successful login
            }
            else
            {
                // Optionally, display a login failed message. Make sure to add a Literal or Label control with ID="Message" in your ASPX page.
                ExceptionLabel.Visible = true; // Make sure the label is visible
                ExceptionLabel.Text = "Prisijungti nepavyko. Bandykite dar kartą.";
            }
        }

        protected void LinkRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}
