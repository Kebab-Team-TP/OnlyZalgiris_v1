<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Zalgiris.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
