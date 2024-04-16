<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.cs" Inherits="Zalgiris.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Custom CSS to increase the size of form elements */
        .form-outline label,
        .form-outline input,
        .form-check-input {
            font-size: 1.5rem; /* Increase font size */
            padding: 0.75rem; /* Increase padding */
        }

        .custom-btn {
            font-size: 2rem; /* Increase font size for button */
            padding: 1rem 2.25rem; /* Increase padding for button */
        }

        h3.text-dark {
            font-size: 2.25rem; /* Increase font size for header */
            margin-bottom: 2rem; /* Increase bottom margin for header */
            font-weight: 700;
        }

        .link-secondary {
            font-size: 1.5rem; /* Increase font size for link */
        }
    </style>
    <form id="registratrionForm" method="post" action="Register.aspx">
        <div style="margin: 15px auto; width: 250px">
            <h3 class="text-dark text-center">Registracija</h3>
            <div>
                <asp:Label ID="ExceptionLabel" runat="server" Text="" Style="color: red; font-size: larger;"></asp:Label>
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
            <div class="form-outline mb-4">
                <input type="checkbox" class="form-check-input" id="exampleCheck1" required="true" />
                <label class="form-check-label" for="exampleCheck1">Sutinku su svetainės taisyklėmis</label>
            </div>

            <input id="Submit1" type="submit" class="btn btn-primary custom-btn" value="Registruotis" />
            <p>&nbsp;&nbsp;</p>
            <div class="form-outline mb-4">
                <a href="Login.aspx" class="link-secondary">Jau turite paskyrą? Prisijunkite!</a>
            </div>
            <p>&nbsp;&nbsp;</p>
        </div>
    </form>

</asp:Content>
