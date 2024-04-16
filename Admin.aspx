<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Zalgiris.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="position: center" class="header-text display-4">User Management</h2>
    <div class="table-responsive">
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="true"></asp:Label>

        <div class="admin-button-master d-flex justify-content-end mb-2">
            <button type="button" id="btnCreateUser" class="btn btn-primary" onclick="openModal()">Create User</button>

            <asp:DropDownList ID="ddlSortOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSortOrder_SelectedIndexChanged">
                <asp:ListItem Text="Sort..." Value=""></asp:ListItem>
                <asp:ListItem Text="Ascending" Value="Asc"></asp:ListItem>
                <asp:ListItem Text="Descending" Value="Desc"></asp:ListItem>
            </asp:DropDownList>

            <asp:DropDownList ID="ddlUserTypeFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserTypeFilter_SelectedIndexChanged">
                <asp:ListItem Text="Filter Users..." Value="All"></asp:ListItem>
                <asp:ListItem Text="Admin Users" Value="Admin"></asp:ListItem>
                <asp:ListItem Text="Non-Admin Users" Value="NonAdmin"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnDeleteSelected" runat="server" Text="Delete Selected Users" CssClass="btn btn-danger" OnClick="btnDeleteSelected_Click" DataKeyNames="Username" />

            <asp:Button ID="btnSaveChanges" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" Enabled="false" />
        </div>

        <asp:GridView ID="UsersGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="UsersGridView_RowCommand" DataKeyNames="Username">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:TemplateField HeaderText="Password">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Generate" CommandName="GeneratePassword" CommandArgument='<%# Eval("Username") %>' CssClass="btn btn-primary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:TemplateField HeaderText="IsAdmin">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsAdmin" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsAdmin")) %>' Enabled="true" onchange="toggleSaveButtonVisibility()" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Delete" CommandName="DeleteUser" CommandArgument='<%# Eval("UserName") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- The Modal -->
    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <h2>Create New User</h2>
            <div>
                <label>Username:</label>
                <asp:TextBox ID="txtNewUsername" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtNewEmail">Email:</label>
                <asp:TextBox ID="txtNewEmail" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="generatedPassword" runat="server" Visible="true" Text="Generated password:"></asp:Label>
                <button type="button" onclick="generatePassword(16)">Generate Password</button>
            </div>
            <div>

                <asp:Button ID="btnAddUsr" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddUser_Click" Enabled="true" />
            </div>
        </div>
    </div>

    <script>
        function toggleSaveButtonVisibility() {
            var saveButton = document.getElementById('<%= btnSaveChanges.ClientID %>');
            var checkboxes = document.querySelectorAll('[id*=chkIsAdmin]');
            var anyChecked = Array.from(checkboxes).some(function (checkbox) {
                return checkbox.checked;
            });

            if (saveButton) {
                saveButton.disabled = !anyChecked; // Enable the Save button if any checkbox is checked
            }
        }
    </script>

    <script>
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("btnCreateUser");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        function closeModal() {
            modal.style.display = "none";
        }

        function openModal() {
            modal.style.display = "block";
        }

        // Generate a random password
        // Generate a random password
        function generatePassword(length) {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var newPassword = "";

            for (var i = 0; i < length; i++) {
                newPassword += chars.charAt(Math.floor(Math.random() * chars.length));
            }

            var generatedPasswordElement = document.getElementById("<%= generatedPassword.ClientID %>");
            if (generatedPasswordElement) {
                generatedPasswordElement.innerText = "Generated password: " + newPassword;
            } else {
                console.error("Element with ID 'generatedPassword' not found.");
            }
        }
    </script>
</asp:Content>
