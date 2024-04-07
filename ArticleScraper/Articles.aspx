<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Articles.aspx.cs" Inherits="Zalgiris.ArticleScraper.Articles" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Naujausios naujienos</h1>
        <asp:Repeater ID="rptResults1" runat="server">
            <ItemTemplate>
                <div class="card">
                    <div class ="row">
                        <div class="col-md-4">
                            <img class="img-fluid rounded-start"  src=<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).ImageUrl %>>
                        </div>
                        <div class="col-md-8">
                            <a href=<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).ArticleUrl %>><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Name %></a>
                            <p><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Date %></p> 
                            <p><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Description %></p>
                        </div>
                    </div> 
                </div>
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>
