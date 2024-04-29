<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Articles.aspx.cs" Inherits="Zalgiris.ArticleScraper.Articles" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="schedule-header">Naujausi Straipsniai</h1>
    <asp:Repeater ID="rptResults1" runat="server">
        <ItemTemplate>
            <div class="outer-card-padding">
                <div class="card">
                    <!-- Triangle -->
                    <div class="card-triangle"></div>

                    <!-- Logo -->
                    <%--<img class="article-logo" src="BasketNewsLogo.png" alt="Zalgiris.lt">--%>
                    <img class="article-logo" src="<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Source == Zalgiris.ArticleScraper.ArticleSources.Zal ? "ZalgirisLogo.png" : "BasketNewsLogo.png" %>" alt="<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Source == Zalgiris.ArticleScraper.ArticleSources.Zal ? "Zalgiris.lt" : "BasketNews.com" %>">
                    <div class="row">


                        <div class="col-md-4">
                            <img class="img-fluid rounded-start article-image" src="<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).ImageUrl %>">
                        </div>
                        <div class="col-md-8">
                            <div class="pt-3">
                                <a class="card-article-title" href="<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).ArticleUrl %>"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Name %></a>
                                <p class="card-date"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Date %></p>
                                <p class="card-text"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Description %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>
    <table class="pageNumbers">
        <asp:Repeater ID="rptPagination" runat="server">
            <ItemTemplate>
                <td>
                    <asp:Panel runat="server" Visible='<%# Eval("IsPageNumber") %>'>
                        <asp:LinkButton runat="server" Text='<%# Eval("PageNumber") %>' CssClass='<%# Convert.ToInt32(Eval("PageNumber")) == currentPage ? "activePage" : "pageNumber" %>' OnClick="Page_Changed"></asp:LinkButton>
                    </asp:Panel>
                    <asp:Panel runat="server" Visible='<%# !Convert.ToBoolean(Eval("IsPageNumber")) %>'>
                        <span class="pageNumber">...</span>
                    </asp:Panel>
                </td>

            </ItemTemplate>
        </asp:Repeater>
    </table>


</asp:Content>
