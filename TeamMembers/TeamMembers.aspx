<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TeamMembers.aspx.cs" Inherits="Zalgiris.TeamMembers.TeamMembers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container-fostrap {
            display: table-cell;
            padding: 1em;
            text-align: center;
            vertical-align: middle;
        }

        .fostrap-logo {
            width: 100px;
            margin-bottom: 15px
        }

        h1.heading {
            color: #fff;
            font-size: 1.15em;
            font-weight: 900;
            margin: 0 0 0.5em;
            color: #505050;
        }

        @media (min-width: 450px) {
            h1.heading {
                font-size: 3.55em;
            }
        }

        @media (min-width: 760px) {
            h1.heading {
                font-size: 3.05em;
            }
        }

        @media (min-width: 900px) {
            h1.heading {
                font-size: 3.25em;
                margin: 0 0 0.3em;
            }
        }

        .card {
            display: block;
            margin-bottom: 20px;
            line-height: 1.42857143;
            background-color: #fff;
            border-radius: 2px;
            box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12);
            transition: box-shadow .25s;
        }

            .card:hover {
                box-shadow: 0 8px 17px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            }

        .img-card {
            width: 100%;
            height: 200px;
            border-top-left-radius: 2px;
            border-top-right-radius: 2px;
            display: block;
            overflow: hidden;
        }

            .img-card img {
                width: 100%;
                height: 200px;
                object-fit: cover;
                transition: all .25s ease;
            }

        .card-content {
            padding: 15px;
            text-align: left;
        }

        .card-title {
            margin-top: 0px;
            font-weight: 700;
            font-size: 1.65em;
        }

            .card-title a {
                color: #000;
                text-decoration: none !important;
            }

        .card-read-more {
            border-top: 1px solid #D4D4D4;
        }

            .card-read-more a {
                text-decoration: none !important;
                padding: 10px;
                font-weight: 600;
                text-transform: uppercase
            }
        .card-text{
            font-weight: 500;
            font-size: 1.4em;
        }
        .card-number {
            position: absolute;
            top: 10px; /* Adjust as needed */
            left: 10px; /* Adjust as needed */
            padding: 5px; /* Add padding for better visibility */
            font-size: 20px;
            font-weight: 600;
            color: #3db36a;
        }
        .bg-zalgiris-darker{
            background-color: #215f38;
        }
        .bg-zalgiris{
            background-color: #3db36a;
        }
    </style>
    <p class="zalia-balta" style="text-align: center">Komandos Nariai</p>
    <div class="container">
        <div class="row">
            <% foreach (var member in playerList) { %>
                    <div class="col-xs-12 col-sm-4">
                        <div class="card">
                            <div class="card-number">
                                <%="#" + member.Number %>
                            </div>
                            <img src="<%= member.ImageLink %>" alt="<%= member.Name %>">
                            <div class="card-content bg-zalgiris text-white text-center">
                                <h5 class="card-title"><%= member.Name %></h5>
                                <p class="card-text"><%= member.Position %></p>
                            </div>
                        </div>
                    </div>
            <% } %>
        </div>
        <div class ="row">
            <% foreach (var member in staffList) { %>
                <div class="col-xs-12 col-sm-4">
                    <div class ="card">
                            <img src="<%= member.ImageLink %>" alt="<%= member.Name %>">
                            <div class="card-content bg-zalgiris-darker text-white text-center">
                                <h5 class="card-title"><%= member.Name %></h5>
                                <p class="card-text"><%= member.Role %></p>
                            </div>
                     </div>
                 </div> 
            <% } %>
        </div>
    </div>
</asp:Content>