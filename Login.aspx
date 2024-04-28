<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Zalgiris.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="background-login-container">
        <form id="loginForm" method="post" action="Login.aspx">
            <div class="row align-content-center">
                <div class="mt-5">
                    <div>
                        <asp:Label ID="Label1" runat="server" Text=""
                            Style="color: red; font-size: larger;"></asp:Label>
                    </div>
                    <div style="margin: 15px auto; width: 250px">

                        <div class="container rounded-5" style="background-color: rgba(38, 38, 38, 0.8)">
                            <div class="header-text">
                                <h1 class="display-5">Prisijungimas</h1>
                            </div>
                            <div>
                                <asp:Label ID="ExceptionLabel" runat="server" Text=""
                                    Style="color: red; font-size: 1.5rem;"></asp:Label>
                            </div>
                            <div class="form-outline mb-4">
                                <label for="input_username">Naudotojo vardas</label>
                                <input type="text" class="form-control" id="input_username" name="username" placeholder="Vardas" />
                            </div>
                            <div class="form-outline mb-4">
                                <label for="input_password">Slaptažodis</label>
                                <input type="password" class="form-control" id="input_password" name="password" placeholder="Slaptažodis" />
                            </div>
                            <input id="Submit1" type="submit" class="btn btn-primary btn-zalgiris" value="Prisijungti" />
                            <p>&nbsp;&nbsp;</p>
                            <div class="form-outline mb-4">
                                <a href="Register.aspx" class="link-success" style="font-size: 1.5rem">Neturite paskyros? Užsiregistruokite!</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
