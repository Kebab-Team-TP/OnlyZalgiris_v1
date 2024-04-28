<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Zalgiris.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <form id="registratrionForm" method="post" action="Register.aspx">
        <div class="background-login-container">
            <div class="row align-content-center">
                <div class="mt-5">
                    <div style="margin: 15px auto; width: 250px">
                        <div class="container rounded-5" style="background-color: rgba(38, 38, 38, 0.8)">
                            <p>&nbsp;&nbsp;</p>
                            <h1 class="display-5">Registracija</h1>
                            <div>
                                <asp:Label ID="ExceptionLabel" runat="server" Text="" Style="color: red; font-size: 1.5rem;"></asp:Label>
                            </div>

                            <div class="form-outline mb-4">
                                <label for="input_email">Elektroninis paštas</label>
                                <input type="text" class="form-control" id="input_email" name="email" placeholder="El. paštas" />
                            </div>
                            <div class="form-outline mb-4">
                                <label for="input_username">Vartotojo vardas</label>
                                <input type="text" class="form-control" id="input_username" name="username" placeholder="Username" />
                            </div>

                            <div class="form-outline mb-4">
                                <label for="input_password">Slaptažodis</label>
                                <input type="password" class="form-control" id="input_password" name="password" placeholder="Password" />
                            </div>
                            <div class="form-checkbox mb-4">
                                <input type="checkbox" class="form-check-input align-middle" id="exampleCheck1" required="true" />
                                <label class="form-check-label" style="margin-right: 3px" for="exampleCheck1">Sutinku su svetainės taisyklėmis</label>
                            </div>

                            <input id="Submit1" type="submit" class="btn btn-primary btn-zalgiris" value="Registruotis" />
                            <p>&nbsp;&nbsp;</p>
                            <div class="form-outline mb-4">
                                <a href="Login.aspx" class="link-success" style="font-size: 1.5rem">Jau turite paskyrą? Prisijunkite!</a>
                            </div>
                            <p>&nbsp;&nbsp;</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </form>

</asp:Content>
