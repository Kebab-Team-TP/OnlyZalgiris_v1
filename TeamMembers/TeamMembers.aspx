<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TeamMembers.aspx.cs" Inherits="Zalgiris.TeamMembers.TeamMembers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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