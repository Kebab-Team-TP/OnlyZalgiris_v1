<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Articles.aspx.cs" Inherits="Zalgiris.ArticleScraper.Articles" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="schedule-header">Naujausi Straipsniai</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8">
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
                                            <div style="text-align: left">
                                                <a class="card-article-title justify-content-start" style="text-align: left" href="<%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).ArticleUrl %>"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Name %></a>
                                            </div>
                                            <p class="card-date text-start"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Date %></p>
                                            <p class="card-text text-start pb-3"><%# ((Zalgiris.ArticleScraper.Article)Container.DataItem).Description %></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-md-4">
                <div class='sk-ww-twitter-list' data-embed-id='25411776'></div>
                <script src='https://widgets.sociablekit.com/twitter-list/widget.js' async defer></script>
            </div>
        </div>
    </div>

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
