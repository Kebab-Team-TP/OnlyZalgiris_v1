<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Zalgiris.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /*Style čia įdėjau, jei kartais scale'inimas taikomas per mažas visiems puslapiam, o ne tik šitam*/
        /* Custom CSS to increase the size of form elements */
        .form-outline label,
        .form-outline input,
        .form-check-label {
            font-size: 1.75rem; /* Increase font size */
            padding: 1rem; /* Increase padding */
        }

        .custom-btn {
            font-size: 2rem; /* Increase font size for button */
            padding: 1rem 2.25rem; /* Increase padding for button */
        }

        .link-secondary {
            font-size: 1.5rem; /* Increase font size for link */
        }

        h3.text-dark {
            font-size: 2.25rem; /* Increase font size for header */
            margin-bottom: 2rem; /* Increase bottom margin for header */
            font-weight: 700;
        }
    </style>
    <form id="loginForm" method="post" action="Login.aspx">
        <div>
            <asp:Label ID="Label1" runat="server" Text=""
                Style="color: red; font-size: larger;"></asp:Label>

        </div>
        <div style="margin: 15px auto; width: 250px">
            <div>
                <asp:Label ID="ExceptionLabel" runat="server" Text=""
                    Style="color: red; font-size: larger;"></asp:Label>

            </div>
            <h3 class="text-dark text-center">Prisijungimas</h3>
            <div class="form-outline mb-4">
                <label for="input_username">Naudotojo vardas</label>
                <input type="text" class="form-control" id="input_username" name="username" placeholder="Vardas" />
            </div>
            <div class="form-outline mb-4">
                <label for="input_password">Slaptažodis</label>
                <input type="password" class="form-control" id="input_password" name="password" placeholder="Slaptažodis" />
            </div>
            <input id="Submit1" type="submit" class="btn btn-primary custom-btn" value="Prisijungti" />
            <p>&nbsp;&nbsp;</p>
            <div class="form-outline mb-4">
                <a href="Register.aspx" class="link-secondary">Neturite paskyros? Užsiregistruokite!</a>
            </div>
            <p>&nbsp;&nbsp;</p>
        </div>

    </form>
</asp:Content>
