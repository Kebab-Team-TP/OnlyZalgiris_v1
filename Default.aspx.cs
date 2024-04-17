using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using HtmlAgilityPack;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Web;
using Zalgiris.Models;

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
        private BrowserType GetBrowserType(HttpRequest request)
        {
            string userAgent = request.UserAgent;

            if (userAgent.Contains("Edg"))
            {
                // "Edg" is a unique identifier for Microsoft Edge based on Chromium
                return BrowserType.Edge;
            }
            else if (userAgent.Contains("Firefox"))
            {
                return BrowserType.Firefox;
            }
            else if (userAgent.Contains("Chrome"))
            {
                return BrowserType.Chrome;
            }
            else
            {
                // Default to Edge or handle other cases
                return BrowserType.Edge;
            }
        }



        private async Task DisplayLiveScores()
        {
            string url = "https://www.livescore.in/lt/komanda/zalgiris-kaunas/6JbDYvUs/";
            BrowserType browserType = GetBrowserType(Request);
            string html = await CallUrlUsingSelenium(url, browserType);
            string liveScores = ParseHtmlForLiveScores(html);
            LiveScoresLiteral.Text = liveScores;
        }

        private async Task<string> CallUrlUsingSelenium(string fullUrl, BrowserType browserType)
        {
            IWebDriver driver;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    string chromeDriverPath = Server.MapPath("~/Drivers/chromedriver.exe");
                    driver = new ChromeDriver(chromeDriverPath);
                    break;
                case BrowserType.Firefox:
                    string firefoxDriverPath = Server.MapPath("~/Drivers/geckodriver.exe");
                    driver = new FirefoxDriver(firefoxDriverPath);
                    break;
                case BrowserType.Edge:
                    string edgeDriverPath = Server.MapPath("~/Drivers/edgedriver_win64/msedgedriver.exe");
                    driver = new EdgeDriver(edgeDriverPath);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type");
            }

            using (driver)
            {
                driver.Navigate().GoToUrl(fullUrl);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                return driver.PageSource;
            }
        }





        private string ParseHtmlForLiveScores(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode liveMatchNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'event__match--live')]");
            if (liveMatchNode == null) return "No live match currently.";

            var homeTeamName = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__participant--home')]").InnerText.Trim();
            var awayTeamName = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__participant--away')]").InnerText.Trim();
            var homeScore = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__score--home')]").InnerText.Trim();
            var awayScore = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__score--away')]").InnerText.Trim();
            var quarterOrTime = liveMatchNode.SelectSingleNode(".//div[contains(@class, 'event__stage--block')]").InnerText.Trim().Replace("\r", "").Replace("\n", ", ");

            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='live-match-info'>")
                   .Append("<h4>Live Match:</h4>")
                   .Append($"<p>{homeTeamName} vs {awayTeamName}</p>")
                   .Append($"<p>Score: {homeScore} - {awayScore}</p>")
                   .Append($"<p>{quarterOrTime}</p>")
                   .Append("</div>");

            return builder.ToString();
        }
        private enum BrowserType
        {
            Chrome,
            Firefox,
            Edge
        }

    }

}
