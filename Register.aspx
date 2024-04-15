<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Zalgiris.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="p-3 p-md-4 p-xl-5">
        <div class="container">
            <div class="row">
                <div class="col-12 col-md-6 ">
                    <div class="d-flex flex-column justify-content-between h-100 p-3 p-md-4 p-xl-5">
                        <h3 class="m-0 text-dark">Sveiki atvykę!</h3>
                        <img class="img-fluid rounded mx-auto my-4" loading="lazy" src="./Images/OnlyZ.png" width="245" height="80" alt="OnlyŽalgiris Logo">
                        <p class="mb-0 text-dark">Jau turite paskyrą? <a href="Login.aspx" class="link-secondary">Prisijunkite</a></p>
                    </div>
                </div>
                <div class="col-12 col-md-6">
                    <div class="p-3 p-md-4 p-xl-5">
                        <div class="row">
                            <div class="col-12">
                                <div class="mb-5">
                                    <h3 class="text-dark">Registracija</h3>
                                </div>
                            </div>
                        </div>
                        <form id="registratrionForm" method="post" action="Register.aspx">
                            <div class="row gy-3 gy-md-4 overflow-hidden">
                                    <div>
                                        <asp:Label ID="ExceptionLabel" runat="server" Text="" Style="color: red; font-size: larger;"></asp:Label>
                                    </div>
                                    <div class="col-12 text-left">
                                        <label for="input_email" class="form-label text-dark">Elektroninis paštas</label>
                                        <input type="text" class="form-control" id="input_email" name="email" placeholder="El. paštas" />
                                    </div>
                                    <div class="col-12 text-left">
                                        <label for="input_username" class="form-label text-dark">Vartotojo vardas</label>
                                        <input type="text" class="form-control" id="input_username" name="username" placeholder="Username" />
                                    </div>

                                    <div class="col-12 text-left">
                                        <label for="input_password" class="form-label text-dark">Slaptažodis</label>
                                        <input type="password" class="form-control" id="input_password" name="password" placeholder="Password" />
                                    </div>
                                    <div class="col-12 text-left">
                                        <input type="checkbox" class="form-check-input" id="exampleCheck1" required="true" />
                                        <label class="form-check-label text-dark" for="exampleCheck1">Sutinku su svetainės taisyklėmis</label>
                                    </div>

                                    <input id="Submit1" type="submit" class="btn bsb-btn-xl btn-success" value="Registruotis" />

                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--<form id="registratrionForm" method="post" action="Register.aspx" >
        <div style="margin:  15px auto;width:250px">
        <div>
            <asp:Label ID="ExceptionLabel" runat="server" Text="" style="color: red; font-size: larger;"></asp:Label>
        </div>
        <div class="form-group">
            <label for="input_email">Elektroninis paštas</label>
            <input type="text" class="form-control" id="input_email" name="email" placeholder="El. paštas"/>
        </div>
        <div class="form-group">
            <label for="input_username">Vartotojo vardas</label>
            <input type="text" class="form-control" id="input_username" name="username" placeholder="Username"/>
        </div>
        
        <div class="form-group">
            <label for="input_password">Slaptažodis</label>
            <input type="password" class="form-control" id="input_password" name="password"  placeholder="Password"/>
        </div>
        <div class="form-check">
            <input type="checkbox" class="form-check-input" id="exampleCheck1" required="true"/>
            <label class="form-check-label" for="exampleCheck1">Sutinku su svetainės taisyklėmis</label>
        </div>
        
        <input id="Submit1" type="submit" class="btn btn-primary" value="Registruotis" />
        
        </div>
    </form>--%>
</asp:Content>
