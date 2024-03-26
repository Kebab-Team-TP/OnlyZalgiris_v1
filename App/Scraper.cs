
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Zalgiris.App
{
    public class Scraper
    {
        /*private async static Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(fullUrl);
                Console.WriteLine(response);
                return response;
            }
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            Label1.Text = "Loaded";
            string url = "https://zalgiris.lt/schedule/bc-zalgiris/";
            var responseTask = await CallUrl(url);
            string txt = ParseHtml(responseTask);
            Label1.Text = txt;
        }

        private string ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNode programmerLinks = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='table-wrapper']");
            if (programmerLinks == null)
                return "";

            return programmerLinks.InnerHtml;
        }*/
    }
}