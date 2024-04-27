using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zalgiris.App;
using Zalgiris.Models;

namespace Zalgiris
{
    public partial class Admin : Page
    {
        public List<User> usersWithIsAdmin = new List<User>();
        CheckBox checkBoxToDisable = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is authenticated and is an admin
                if (Session["authenticated"] != null && (bool)Session["authenticated"] && Session["IsAdmin"] != null && (bool)Session["IsAdmin"])
                {
                    // Fetch all users
                    BindGridView();
                }
                else
                {
                    // Redirect to login page or display access denied message
                    Response.Redirect("~/Login.aspx");
                }

                lblErrorMessage.Text = "Authenticated: " + ((bool)Session["authenticated"]).ToString() + "| IsAdmin:" + ((bool)Session["IsAdmin"]).ToString();
            }
        }

        protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                string username = e.CommandArgument.ToString();

                // Implement user deletion logic
                UsersController.Delete(username);

                // Refresh the GridView after deletion
                List<User> users = UsersController.GetAll();
                UsersGridView.DataSource = users;
                UsersGridView.DataBind();
            }

            else if (e.CommandName == "GeneratePassword")
            {
                string username = e.CommandArgument.ToString();

                // Generate a new password
                string newPassword = GeneratePassword(16);

                // Update the user's password
                UsersController.UpdatePassword(username, newPassword);

                // Display success message
                lblErrorMessage.Text = $"New password has been set for user: {username} and new pass is: {newPassword}";
            }
        }

        private string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string pass = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            if (generatedPassword.Enabled)
            {
                generatedPassword.Text = "Generated password: " + pass;
            }

            return pass;
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<string> usersToDelete = new List<string>();

            // Loop through each row in the GridView
            foreach (GridViewRow row in UsersGridView.Rows)
            {
                // Find the CheckBox in the row
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                // If the CheckBox is checked, add the username to the list of users to delete
                if (chkSelect != null && chkSelect.Checked)
                {
                    int rowIndex = row.RowIndex;
                    if (rowIndex < UsersGridView.DataKeys.Count)
                    {
                        string username = UsersGridView.DataKeys[rowIndex]["Username"].ToString();
                        usersToDelete.Add(username);
                    }
                    else
                    {
                        // Display an error message directly on the page
                        lblErrorMessage.Text = $"Error: Index out of range for row {rowIndex}";
                        return; // Stop processing further
                    }
                }
            }

            // Delete selected users
            foreach (string username in usersToDelete)
            {
                UsersController.Delete(username);
            }

            // Refresh the GridView
            BindGridView();
        }



        protected void BindGridView()
        {
            List<User> users = UsersController.GetAll();

            // Apply sorting based on the selected order
            if (ddlSortOrder.SelectedValue == "Asc")
            {
                users = users.OrderBy(u => u.Username).ToList();
            }
            else if (ddlSortOrder.SelectedValue == "Desc")
            {
                users = users.OrderByDescending(u => u.Username).ToList();
            }

            // Filter users based on the selected filter
            if (ddlUserTypeFilter.SelectedValue == "Admin")
            {
                users = users.Where(u => u.IsAdmin).ToList();
            }
            else if (ddlUserTypeFilter.SelectedValue == "NonAdmin")
            {
                users = users.Where(u => !u.IsAdmin).ToList();
            }

            UsersGridView.DataSource = users;
            UsersGridView.DataBind();
        }

        protected void ddlUserTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void ddlIsAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show the Save button when the user changes the IsAdmin value
            btnSaveChanges.Visible = true;
        }

        protected void ddlSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Re-bind the GridView with the sorted data
            BindGridView();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateUsersWithIsAdmin();

                UsersController.SaveUsers(usersWithIsAdmin);

                BindGridView();

                btnSaveChanges.Enabled = false;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = $"Error saving changes: {ex.Message}";
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            // Get the input values from the form
            string username = txtNewUsername.Text;
            string email = txtNewEmail.Text;
            string password = generatedPassword.Text;//GeneratePassword(16); // Assuming you generate the password in JavaScript

            // Call the AddUser method to add the new user
            AddUser(username, email, password);

            BindGridView();
        }

        protected void UpdateUsersWithIsAdmin()
        {
            usersWithIsAdmin = UsersController.GetAll();
            // Get the username of the currently logged-in user
            string currentUsername = Session["Username"].ToString().Trim(); // Trim any extra spaces

            foreach (GridViewRow row in UsersGridView.Rows)
            {
                // Find the username of the user in this row
                string username = UsersGridView.DataKeys[row.RowIndex]["Username"].ToString().Trim(); // Trim any extra spaces

                // Find the checkbox for this row
                CheckBox chkIsAdmin = (CheckBox)row.FindControl("chkIsAdmin");

                // Enable the checkbox only if the user is not the currently logged-in user
                if (!string.Equals(username, currentUsername, StringComparison.OrdinalIgnoreCase))
                {
                    chkIsAdmin.Enabled = true;

                    // Find the corresponding user in usersWithIsAdmin and update its IsAdmin value
                    User user = usersWithIsAdmin.FirstOrDefault(u => string.Equals(u.Username.Trim(), username, StringComparison.OrdinalIgnoreCase)); // Trim any extra spaces
                    if (user != null)
                    {
                        user.IsAdmin = chkIsAdmin.Checked;
                    }
                }
                else
                {
                    // Disable the checkbox for the current user
                    chkIsAdmin.Enabled = false;
                }
            }
        }

        protected void AddUser(string username, string email, string password)
        {
            try
            {
                // Create a new User object
                User newUser = new User
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    IsAdmin = false
                };

                // Call the Add method from UsersController to save the new user
                UsersController.Add(newUser);

                // Optionally, display a success message
                lblErrorMessage.Text = "User added successfully!";
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as displaying an error message
                lblErrorMessage.Text = "Error adding user: " + ex.Message;
            }
        }
    }
}