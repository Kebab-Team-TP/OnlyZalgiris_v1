<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Zalgiris.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link rel="stylesheet" href="https://unpkg.com/bootstrap@5.3.2/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://unpkg.com/bs-brain@2.0.3/components/logins/login-3/assets/css/login-3.css">--%>

    <section class="p-3 p-md-4 p-xl-5">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-6 ">
                    <div class="d-flex flex-column justify-content-between h-100 p-3 p-md-4 p-xl-5">
                        <h3 class="m-0 text-dark">Sveiki atvykę!</h3>
                        <img class="img-fluid rounded mx-auto my-4" loading="lazy" src="./Images/OnlyZ.png" width="245" height="80" alt="OnlyŽalgiris Logo">
                        <p class="mb-0 text-dark">Neturite paskyros? <a href="Register.aspx" class="link-secondary">Registruokitės dabar</a></p>
                        <%--<asp:LinkButton ID="LinkRegister" runat="server" CssClass="btn btn-primary custom-btn" OnClick="LinkRegister_Click">Užsiregistruoti</asp:LinkButton>--%>
                    </div>
                </div>
                <div class="col-12 col-md-6">
                    <div class="p-3 p-md-4 p-xl-5">
                        <div class="row">
                            <div class="col-12">
                                <div class="mb-5">
                                    <h3 class="text-dark">Prisijungimas</h3>
                                </div>
                            </div>
                        </div>
                        <form id="loginForm" method="post" action="Login.aspx">
                            <div>
                                <asp:Label ID="ExceptionLabel" runat="server" Text=""
                                    Style="color: red; font-size: larger;"></asp:Label>

                            </div>
                            <div class="row gy-3 gy-md-4 overflow-hidden">
                                <div class="col-12 text-left">
                                    <label for="input_username" class="form-label text-dark ">Vartotojo vardas <span class="text-danger">*</span></label>
                                    <input type="text" class="form-control" id="input_username" name="username" placeholder="Vardas" required>
                                </div>
                                <div class="col-12 text-left">
                                    <label for="input_password" class="form-label text-dark">Slaptažodis <span class="text-danger">*</span></label>
                                    <input type="password" class="form-control" id="input_password" name="password" placeholder="***********" required>
                                </div>
                                <div class="col-12">
                                    <div class="d-grid">
                                        <input id="Submit1" type="submit" class="btn bsb-btn-xl btn-success" value="Prisijungti" />
                                        <%--<button class="btn bsb-btn-xl btn-success" type="submit">Prisijungti</button>--%>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <div class="row">
                            <div class="col-12">
                                <hr class="mt-5 mb-4 border-secondary-subtle">
                                <div class="text-left">
                                    <a href="#!" class="link-secondary">Pamiršote slaptažodį? (WiP)</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%-- <form id="loginForm" method="post" action="Login.aspx">
     <div>
         <asp:Label ID="Label1" runat="server" Text=""
             Style="color: red; font-size: larger;"></asp:Label>

     </div>--%>
    <%--<div style="margin: 15px auto;width:250px">
            <div>
                <asp:Label ID="ExceptionLabel" runat="server" Text="" 
    style="color: red; font-size: larger;"></asp:Label>

            </div>
            <div class="form-group">
                <label for="input_username">Naudotojo vardas</label>
                <input type="text" class="form-control" id="input_username" name="username" placeholder="Vardas"/>
            </div>
            <div class="form-group">
                <label for="input_password">Slaptažodis</label>
                <input type="password" class="form-control" id="input_password" name="password" placeholder="Slaptažodis"/>
            </div>
            <input id="Submit1" type="submit" class="btn btn-primary custom-btn" value="Prisijungti" />
        </div>
    </form>
    <br>
<div class="d-flex justify-content-center mt-3"> 
    <span class="mr-2 align-self-center">Neturite paskyros?&nbsp;&nbsp; </span> 
    <asp:LinkButton ID="LinkRegister" runat="server" CssClass="btn btn-primary custom-btn" OnClick="LinkRegister_Click">Užsiregistruoti</asp:LinkButton>
</div>--%>
</asp:Content>
