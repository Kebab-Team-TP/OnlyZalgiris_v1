<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Zalgiris.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="loginForm" method="post" action="Login.aspx" >
        <div style="margin: 15px auto;width:250px">
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
</div>


</asp:Content>
