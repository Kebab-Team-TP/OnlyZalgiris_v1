using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

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
                articles = articles.OrderByDescending(article => DateTime.Parse(article.Date)).ToList();
                rptResults1.DataSource = articles;
                rptResults1.DataBind();
            }
            var testArticles = articles;
        }
        private async Task FindArticles()
        {
            // Zalgiris.lt
            // Find article page count 
            string htmlNr = await CallUrl("https://zalgiris.lt/news/bc-zalgiris/");
            int maxPage = GetPageCountZal(htmlNr);

            // Scrape data from each page
            // Set to fixed number for speed instead of 900-ish
            for (int i = 1; i <= 3; i++)
            {
                string urlZal = "https://zalgiris.lt/news/bc-zalgiris/page/" + i.ToString();
                string htmlZal = await CallUrl(urlZal);
                ParseHtmlZal(htmlZal);
            }
            // BasketNews
            string htmlNrBN = await CallUrl("https://www.basketnews.lt/komandos/265-kauno-zalgiris/naujienos.html");
            int maxPageBN = GetPageCountBN(htmlNrBN);
            string baseUrl = "https://www.basketnews.lt/komandos/265-kauno-zalgiris/naujienos.html";
            // BN calculates page url by articles per page: a full page is concidered 29 articles long
            // Set to fixed number for speed
            for (int i = 0; i <= 2 * 29; i += 29) 
            {
                string urlBN = baseUrl.Replace(".html", $".{i.ToString()}.html");
                string htmlBN = await CallUrl(urlBN);
                ParseHtmlBN(htmlBN);
            }

        }
        private async Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(fullUrl);
            }
        }
        // Parse data from zalgiris.lt
        private void ParseHtmlZal(string html) 
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
                //string date = dateNode?.InnerText.Trim();
                DateTime datetime = DateTime.Parse(dateNode?.InnerText.Trim());
                string date = datetime.ToString("yyyy/MM/dd");
                string description = descriptionNode?.InnerText.Trim();
                string name = nameNode?.InnerText.Trim();
                string articleUrl = articleNode?.GetAttributeValue("href", "");
                Article article = new Article(description, imgUrl, articleUrl, date, name, ArticleSources.Zal);
                articles.Add(article);
            }
        }
        /// <summary>
        /// Get page count from zalgiris.lt
        /// </summary>
        /// <param name="html">html from page</param>
        /// <returns>Number of pages</returns>
        private int GetPageCountZal(string html) 
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
        /// <summary>
        /// Parses data from Basketnews.lt article page
        /// </summary>
        /// <param name="html">html from page</param>
        private void ParseHtmlBN(string html) 
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            // Get Articles from page
            // BN has 29 articles per page
            var articleElements = htmlDoc.DocumentNode.SelectNodes("//div[@class='main_newslist_item']");
            // Page has 6 duplicating articles, so ignore them
            for (int i = 7; i < articleElements.Count; i++) 
            {
                var element = articleElements[i];

                var imgNode = element.SelectSingleNode(".//div[@class='main_newslist_image']//img");
                var articleNode = element.SelectSingleNode(".//div[@class='main_newslist_image']//a");
                var dateNode = element.SelectSingleNode(".//div[@class='date']");
                var descriptionNode = element.SelectSingleNode(".//div[@class='text']");
                var nameNode = element.SelectSingleNode(".//div[@class='title']");
                //var articleNode = element.SelectSingleNode(".//a");

                string imgUrl = "https://www.basketnews.lt" + imgNode?.GetAttributeValue("src", "").Trim();
                string articleUrl = "https://www.basketnews.lt" + articleNode?.GetAttributeValue("href", "");
                //string date = dateNode?.InnerText.Trim();
                // Jei naujas straipsnis rasoma: Prieš xx min., jei taip, rodyti šiandienos datą
                DateTime dateTime;
                string date;
                if (DateTime.TryParseExact(dateNode?.InnerText.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) 
                {
                    date = dateTime.ToString("yyyy/MM/dd");
                }
                else 
                {
                    date = DateTime.Today.ToString("yyyy/MM/dd");
                }
                string description = descriptionNode?.InnerText.Trim();
                string name = nameNode?.InnerText.Trim().Replace("\n", "");
                name = Regex.Replace(name, @"\s+", " ");
                //string articleUrl = articleNode?.GetAttributeValue("href", "");
                Article article = new Article(description, imgUrl, articleUrl, date, name, ArticleSources.BN);
                articles.Add(article);
            }
        }
        /// <summary>
        /// Get page count from basketnews articles
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private int GetPageCountBN(string html) 
        {
            int maxPage = 0;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var allElements = htmlDoc.DocumentNode.SelectNodes("//div[@class='main_paging']//a");
            var numberElements = allElements.Where(node => node.OuterHtml.Contains("naujienos"));
            // Last element is symbol for next page, ignore it
            for (int i = 0; i < numberElements.Count() - 1; i++) 
            {
                int elementNumber = Int32.Parse(numberElements.ElementAt(i)?.InnerText.Trim());
                if (elementNumber > 0 || elementNumber > maxPage)
                    maxPage = elementNumber;
            }

            return maxPage;
        }
    }
}