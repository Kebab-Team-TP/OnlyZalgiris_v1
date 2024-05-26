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
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using OpenQA.Selenium.DevTools.V121.IO;
using System.Web.UI.WebControls;
using System.IO;

namespace Zalgiris
{
    public partial class _Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    await DD();
                }
                catch(Exception ex){
                    LiveScoresLiteral.Text = ex.Message;
                }
                //sutvarkyti!!!
                //RegisterAsyncTask(new PageAsyncTask(DisplayLiveScores));
            }
        }
        private async Task DD()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://sportapi7.p.rapidapi.com/api/v1/sport/basketball/events/live"),
                Headers =
                {
                    { "x-rapidapi-key", "a692ca4ebfmshb8c46d91633cf2dp19aa10jsn2f1f1530f304" },
                    { "x-rapidapi-host", "sportapi7.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                string teamname = "Kauno Žalgiris";
                string pattern = "\"homeTeam\":{\"name\":\"(.+?)\".+?\"awayTeam\":{\"name\":\"(.+?)\".+?.+?homeScore\":{\"current\":(\\d+).+?awayScore\":{\"current\":(\\d+)";
                StringBuilder st= new StringBuilder();
                Match RealMatch = null;
                foreach (Match match in Regex.Matches(body, pattern, RegexOptions.None, TimeSpan.FromSeconds(1)))
                {
                    if (match.Groups[1].ToString().Equals(teamname) || match.Groups[2].ToString().Equals(teamname))
                    {
                       RealMatch = match;
                    }

                }
                if (RealMatch != null)
                {
                    st.Append(String.Format("<div>{0} - {1}</div>",
                    RealMatch.Groups[1], RealMatch.Groups[2]));

                    st.Append(String.Format("<div>{0} - {1}</div>",
                                      RealMatch.Groups[3], RealMatch.Groups[4]));
                }
                else
                    st.Append("<div>No live matches at the moment</div>");
                
                LiveScoresLiteral.Text = st.ToString();
            }

        }
        private async Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(fullUrl);
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

        /// <summary>
        /// Fix for all browsers
        /// </summary>
        /// <returns></returns>
        
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
