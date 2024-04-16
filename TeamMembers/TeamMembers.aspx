<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TeamMembers.aspx.cs" Inherits="Zalgiris.TeamMembers.TeamMembers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%if (string.IsNullOrEmpty(Request.QueryString["member"]))
        {  %>
    <p class="zalia-balta" style="text-align: center">Komandos Nariai</p>
    <div class="container">
        <div class="row">
            <% foreach (var member in playerList)
                { %>
                    <div class="col-xs-12 col-sm-4">
                        <a class="card" href="?member=<%= member.Slug %>">
                            <div class="card-number">
                                <%="#" + member.Number %>
                            </div>
                            <img src="<%= member.ImageLink %>" alt="<%= member.Name %>">
                            <div class="card-content bg-zalgiris text-white text-center">
                                <h5 class="card-title"><%= member.Name %></h5>
                                <p class="card-text"><%= member.Position %></p>
                            </div>
                        </a>
                    </div>
            <% } %>
        </div>
        <div class ="row">
            <% foreach (var member in staffList)
                { %>
                <div class="col-xs-12 col-sm-4">
                    <a class="card" href="?member=<%= member.Slug %>" class ="card">
                            <img src="<%= member.ImageLink %>" alt="<%= member.Name %>">
                            <div class="card-content bg-zalgiris-darker text-white text-center">
                                <h5 class="card-title"><%= member.Name %></h5>
                                <p class="card-text"><%= member.Position %></p>
                            </div>
                     </a>
                 </div> 
            <% } %>
        </div>
    </div>
    <% } else { %>
    <p class="zalia-balta" style="text-align: center"><%= currentMember.Name %></p>
    <div class="container" style="padding-bottom:50px" >
        <div class="row">
            <div class="card"> 

                <div class="row">
                    <div class="col-6">
                        <% if (currentMember is Zalgiris.TeamMembers.Player)
                            {   %>
                
                        <div class="card-number">
                            <%="#" + (currentMember as Zalgiris.TeamMembers.Player).Number %>
                        </div>
                        <% } %>
                        <img src="<%= currentMember.ImageLink %>" alt ="<%= currentMember.Name %>"/>
                    </div>
                    <div class="col-6" style="padding:25px">
                    <%= Statistics %>
                        <div class="info " style="font-size:20px;">
                            <div><b>Pozicija:</b> <%= currentMember.Position %></div>
                            <div><b>Ugis:</b><%= currentMember.Height %>
                            <b>Amžius:</b><% = currentMember.Age %>
                            <b>Svoris:</b><% = currentMember.Weigth %></div>
                            <div style="font-size:16px"> <% = currentMember.Info %></div>
                        </div>
                    </div>
                </div>
                
            </div>
            
        </div>

    </div>
    <%}%>
</asp:Content>