using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using HtmlAgilityPack;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace Zalgiris
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(DisplayLiveScores));
            }
        }
        private async Task DisplayLiveScores()
        {
            string url = "https://www.livescore.in/lt/komanda/northport/t8QRUq2B/";
            string html = await CallUrlUsingSelenium(url);
            string liveScores = ParseHtmlForLiveScores(html);
            LiveScoresLiteral.Text = liveScores;
        }

        private async Task<string> CallUrlUsingSelenium(string fullUrl)
        {
            var service = EdgeDriverService.CreateDefaultService(@"C:\Users\ignas\Downloads\edgedriver_win64");
            var options = new EdgeOptions();
            options.AddArguments("headless");

            using (var driver = new EdgeDriver(service, options))
            {
                driver.Navigate().GoToUrl(fullUrl);

                // Wait for the specific element to ensure the page has loaded
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("event__match--live")));

                var pageSource = driver.PageSource;
                return pageSource; // This is the HTML content
            }
        }



        private string ParseHtmlForLiveScores(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Targeting the div with the 'event__match--live' class to identify live matches
            // Revised XPath to target live match div
            HtmlNode liveMatchNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'event__match--live')]");



            if (liveMatchNode == null)
            {
                return "No live match currently.";
            }

            // Extracting the specific details based on the structure you've provided
            var homeTeamName = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__participant--home')]")?.InnerText.Trim();
            var awayTeamName = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__participant--away')]")?.InnerText.Trim();
            var homeScore = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__score--home')]")?.InnerText.Trim();
            var awayScore = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__score--away')]")?.InnerText.Trim();
            var quarterOrTime = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__stage--block')]")?.InnerText.Trim().Replace("\r", "").Replace("\n", ", ");

            // Building the output HTML with the extracted information
            StringBuilder builder = new StringBuilder();
            builder.Append($"<div class='live-match-info'>");
            builder.Append($"<h4>Live Match:</h4>");
            builder.Append($"<p>{homeTeamName} vs {awayTeamName}</p>");
            builder.Append($"<p>Score: {homeScore} - {awayScore}</p>");
            builder.Append($"<p>{quarterOrTime}</p>");
            builder.Append($"</div>");

            return builder.ToString();
        }


    }
}