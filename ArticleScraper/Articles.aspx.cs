using HtmlAgilityPack;
using Newtonsoft.Json;
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
        protected List<Article> articlesJSON = new List<Article>();
        protected int currentPage = 1;
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if it's a postback to prevent reloading on every postback
            if (!IsPostBack)
            {
                await FindArticles();
                BindArticles(currentPage);
                BindPageinationControls(currentPage);
            }
        }
        private void BindArticles(int page) 
        {
            var articles = ArticleController.GetAll().OrderByDescending(article => DateTime.Parse(article.Date));
            int articleCount = articles.Count();
            int startIndex = (page - 1) * 25; // 25 articles per page
            int endIndex = Math.Min(startIndex + 24, articleCount - 1);
            
            var articlesToDisplay = articles.Skip(startIndex)
                .Take(endIndex - startIndex + 1).ToList();
            rptResults1.DataSource = articlesToDisplay;
            rptResults1.DataBind();

        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            LinkButton btn = (sender as LinkButton);
            currentPage = int.Parse(btn.Text);
            BindArticles(currentPage);
            BindPageinationControls(currentPage);
        }
        private void BindPageinationControls(int currentPage) 
        {
            var articles = ArticleController.GetAll().OrderByDescending(article => DateTime.Parse(article.Date));
            int articleCount = articles.Count();

            int totalPages = (int)Math.Ceiling((double)articleCount / 25);
            List<object> pageNumbers = new List<object>();

            // Add ellipsis and page numbers
            const int numAdjacentPages = 2; // Number of adjacent pages to display on each side of the current page
            const int numPagesAroundEllipsis = 1; // Number of pages around the ellipsis to display
            const int minPageNumber = 1;

            // Add the first page
            pageNumbers.Add(new PaginationItem { PageNumber = 1, IsPageNumber = true });

            // Add ellipsis if needed
            if (currentPage - numAdjacentPages > minPageNumber + numPagesAroundEllipsis)
            {
                pageNumbers.Add(new PaginationItem { PageNumber = null, IsPageNumber = false }); // Ellipsis
            }

            // Add adjacent pages
            for (int i = Math.Max(minPageNumber + numPagesAroundEllipsis, currentPage - numAdjacentPages);
                     i <= Math.Min(currentPage + numAdjacentPages, totalPages); i++)
            {
                pageNumbers.Add(new PaginationItem { PageNumber = i, IsPageNumber = true });
            }

            // Add ellipsis if needed
            if (currentPage + numAdjacentPages < totalPages - numPagesAroundEllipsis)
            {
                pageNumbers.Add(new PaginationItem { PageNumber = null, IsPageNumber = false }); // Ellipsis
            }

            // Add the last page
            if (totalPages > minPageNumber)
            {
                pageNumbers.Add(new PaginationItem { PageNumber = totalPages, IsPageNumber = true });
            }

            rptPagination.DataSource = pageNumbers;
            rptPagination.DataBind();
        }
        private async Task FindArticles()
        {
            // Zalgiris.lt
            // Find article page count 
            string htmlNr = await CallUrl("https://zalgiris.lt/news/bc-zalgiris/");
            int maxPage = GetPageCountZal(htmlNr);
            bool stopScraping = false;
            // Scrape data from each page
            // Set to fixed number for speed instead of 900-ish
            for (int i = 1; i <= maxPage; i++)
            {

                if (stopScraping)
                {
                    break;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ZAL: {0} is {1} psl.", i, maxPage);
                    string urlZal = "https://zalgiris.lt/news/bc-zalgiris/page/" + i.ToString();
                    string htmlZal = await CallUrl(urlZal);
                    ParseHtmlZal(htmlZal, out stopScraping);
                }
            }
            // BasketNews
            string htmlNrBN = await CallUrl("https://www.basketnews.lt/komandos/265-kauno-zalgiris/naujienos.html");
            int maxPageBN = GetPageCountBN(htmlNrBN);
            string baseUrl = "https://www.basketnews.lt/komandos/265-kauno-zalgiris/naujienos.html";
            // BN calculates page url by articles per page: a full page is concidered 29 articles long
            // Set to fixed number for speed
            stopScraping = false;
            for (int i = 0; i <= maxPageBN*29; i += 29) 
            {
                
                if (stopScraping)
                {
                    break;
                }
                else 
                {
                    System.Diagnostics.Debug.WriteLine("BN: {0} is {1} psl.", i, maxPageBN*29);
                    string urlBN = baseUrl.Replace(".html", $".{i.ToString()}.html");
                    string htmlBN = await CallUrl(urlBN);
                    ParseHtmlBN(htmlBN, out stopScraping);
                }
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
        private void ParseHtmlZal(string html, out bool noNewArticles) 
        {
            noNewArticles = false;
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
                //articles.Add(article);

                // If article exists in JSON, stop script, because remaining articles will already be added
                List<Article> currArticles = ArticleController.GetAll();
                bool articleExists = currArticles.Any(a => a.Name == article.Name && a.Description == article.Description);
                if (!articleExists)
                {
                    ArticleController.Add(article);
                }
                else 
                {
                    noNewArticles = true;
                    break;
                }
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
        private void ParseHtmlBN(string html, out bool noNewArticles) 
        {
            noNewArticles = false;
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
                //articles.Add(article);
                // If article exists in JSON, stop script, because remaining articles will already be added
                List<Article> currArticles = ArticleController.GetAll();
                bool articleExists = currArticles.Any(a => a.Name == article.Name && a.Description == article.Description);
                if (!articleExists)
                {
                    ArticleController.Add(article);
                }
                else
                {
                    noNewArticles = true;
                    break;
                }
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