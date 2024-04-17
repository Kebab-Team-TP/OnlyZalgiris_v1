<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Zalgiris._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="background-container">
        <div class="container text-center">
            <!-- Live Scores / No Match Message -->
            <!-- Live Scores / No Match Message -->

            <div id="liveScoresContainer" class="live-scores-alert">
                <asp:Literal ID="LiveScoresLiteral" runat="server"></asp:Literal>
            </div>
            <!-- Header -->
            <div class="header-text">
                <h1 class="display-4">Welcome to Only<span class="zalgiris-color">Žalgiris</span></h1>
                <p class="lead">Your source for all things related to Žalgiris basketball team.</p>
            </div>
            </div>
            <div class="row justify-content-center">
                <div class="mt-5">
                    <div id="about-tabs">
                        <div class="container">
                            <div class="about-list">
                                <div>
                                    <i class="fa-solid fa-basketball"></i>
                                    <p>Welcome to the ultimate destination for all things Žalgiris! Dive into the heart of basketball greatness with our website, meticulously designed to bring you the pulse of the team's triumphs and journey.</p>
                                </div>
                                <div>
                                    <i class="fa-solid fa-chart-simple"></i>
                                    <p>Stay ahead of the game with real-time updates on the team's latest results, upcoming match schedules, and comprehensive player statistics across various leagues. We're your go-to source for everything Žalgiris, delivering the freshest news and fan-centric articles to keep you in the loop.</p>
                                </div>
                                <div>
                                    <i class="bi bi-rocket-takeoff-fill"></i>
                                    <p>No more endless scrolling through the vast expanse of the internet! We've curated all the content you crave in one convenient spot, so you can effortlessly follow your favorite team without missing a beat. Powered by passion and precision, we're committed to delivering top-tier performance both on and off the court. Join us on this exhilarating journey as we celebrate Žalgiris and its devoted fanbase. Let's make every moment count!<br /><span class="zalia-balta">"<span class="zalgiris-color">Žalia</span> - Balta"</span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container text-center">
            <!--ADD MORE CONTENT HERE IF NEEDED-->
            </div>
    </div>
</asp:Content>
