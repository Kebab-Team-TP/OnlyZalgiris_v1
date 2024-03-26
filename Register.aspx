<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Zalgiris.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <form id="registratrionForm" method="post" action="Register.aspx" >
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
    </form>
    
</asp:Content>