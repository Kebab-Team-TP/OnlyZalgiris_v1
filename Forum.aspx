<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="Zalgiris.Forum" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .forum-container{
            margin:auto;
            max-width:500px;
            height:500px;
            border:1px solid black;
            border-radius:12px;
            color:black;
            margin-bottom:25px;
        }
        .message-container{
            
        }
        .message-block{
            
            max-width:70%;
            display:block;
            margin:5px;
            padding:5px;
            text-align:left;

        }
        .message-block p{
            display:inline-block;
            margin:5px;
            padding:5px;
            width:contain;
            background:#f2f2f2;
            border-radius:5px;
        }
        .message-block.author{
            margin-left:auto;
            text-align: right;
        }
        .message-block .date{
            font-size:8px;
            display:block;
        }
        .message-block.author p{
            background:#006d3e;
            color:white;
            border-radius:5px;
        }
        .forum-input{
            max-width:100% !important;
            width:100%;
            height:35px;
            background: #0000;
            border:none;
            border-top:3px solid black;
            padding-bottom:7px;
            font-size:15px;
            padding-left:12px;
            padding-right:12px;
            border-radius:0;
            border-bottom-right-radius:12px;
            border-bottom-left-radius:12px;
        }
        #MainContent_MessagesField{
            height:465px;
            display:block;
            overflow-y:auto;
        }
        .d-none{
            display:none;
        }
        .forum-header{
            color:black;
            margin:25px;
        }
    </style>
    <div class="container">
    <h2 class="forum-header "><%: Title %></h2>
            <div class="forum-container">
                <asp:Label ID="MessagesField" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="MessageText" CssClass="forum-input" runat="server" OnTextChanged="MessageText_TextChanged"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="d-none" />
            </div >
            

    </div>
</asp:Content>