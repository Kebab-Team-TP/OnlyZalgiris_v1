using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zalgiris.ArticleScraper
{
    public partial class Articles : System.Web.UI.Page
    {
        protected List<Article> articles = new List<Article>();
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if it's a postback to prevent reloading on every postback
            if (!IsPostBack)
            {
                await FindArticles();
                rptResults1.DataSource = articles;
                rptResults1.DataBind();
            }
            var testArticles = articles;
        }
        private async Task FindArticles()
        {
            // Find article page count
            string htmlNr = await CallUrl("https://zalgiris.lt/news/bc-zalgiris/");
            int maxPage = GetPageCount(htmlNr);

            // Scrape data from each page
            // Set to 20 for speed instead of 900-ish
            for (int i = 1; i <= 20; i++)
            {
                string url = "https://zalgiris.lt/news/bc-zalgiris/page/" + i.ToString();
                string html = await CallUrl(url);
                ParseHtml(html);
            }
        }
        private async Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(fullUrl);
            }
        }
        private void ParseHtml(string html) 
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            // Get Articles from page
            // Zalgiris.lt has 9 articles per page
            var articleElements = htmlDoc.DocumentNode.SelectNodes("//li[@class='item']");
            foreach (var element in articleElements) 
            {
                var imgNode = element.SelectSingleNode(".//div[@class='featured-centered']");
                var dateNode = element.SelectSingleNode(".//div[@class='date']");
                var descriptionNode = element.SelectSingleNode(".//div[@class='excerpt']");
                var nameNode = element.SelectSingleNode(".//h2");
                var articleNode = element.SelectSingleNode(".//a");

                string imgUrl = imgNode?.GetAttributeValue("img-lazy-retina", "");
                string date = dateNode?.InnerText.Trim();
                string description = descriptionNode?.InnerText.Trim();
                string name = nameNode?.InnerText.Trim();
                string articleUrl = articleNode?.GetAttributeValue("href", "");
                Article article = new Article(description, imgUrl, articleUrl, date, name);
                articles.Add(article);
            }
        }
        private int GetPageCount(string html) 
        {
            int maxPage = 0;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var numberElements = htmlDoc.DocumentNode.SelectNodes("//a[@class='page-numbers']");
            foreach (var item in numberElements)
            {
                int elementNumber = Int32.Parse(item?.InnerText.Trim());
                if (elementNumber > 0 || elementNumber > maxPage)
                    maxPage = elementNumber;
            }
            return maxPage;
        }
    }
}