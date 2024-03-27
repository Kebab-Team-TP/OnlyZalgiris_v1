<%@ Page Title="Schedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Zalgiris.Schedule" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="schedule-header"><%: Title %></h2>
    <p>&nbsp;</p>
    <div class="table-container">
        <div class="schedule-table">
            <table class="schedule">
                <colgroup>
                    <!-- Define column widths here -->
                    <col style="width: auto;" />
                    <col style="width: auto;" />
                    <col style="width: auto;" />
                    <col style="width: auto;" />
                    <col style="width: auto;" />
                    <col style="width: auto" /> <!-- Adjust this width as needed -->
                </colgroup>
                <thead>
                    <tr>
                        <th style="width: auto;">Data</th>
                        <th style="width: auto;">Komandos</th>
                        <th style="width: auto;">Lyga</th>
                        <th style="width: auto;">Transliuojama</th>
                        <th style="width: auto;">Vieta</th>
                        <th style="width: auto;">Bilietai</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="ScheduleLiteral"></asp:Literal>
                </tbody>
            </table>
        </div>
    </div>
    <!--<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>-->
    <p>
    </p>
</asp:Content>
