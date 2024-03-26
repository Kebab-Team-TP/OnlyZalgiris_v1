using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zalgiris
{
    public partial class Schedule : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if it's a postback to prevent reloading on every postback
            if (!IsPostBack)
            {
                await DisplaySchedule();
            }
        }

        private async Task DisplaySchedule()
        {
            string url = "https://zalgiris.lt/schedule/bc-zalgiris/";
            string html = await CallUrl(url);
            string parsedHtml = ParseHtml(html);
            Label2.Text = parsedHtml;
        }

        private async Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(fullUrl);
            }
        }

        private string ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNode programmerLinks = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='table-wrapper']");
            return programmerLinks?.InnerHtml ?? "Schedule not found";
        }
    }
}