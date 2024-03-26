using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zalgiris
{
    public partial class Schedule : Page
    {
        /*protected async void Page_Load(object sender, EventArgs e)
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
        }*/
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
            ScheduleLiteral.Text = parsedHtml;
        }

        private async Task<string> CallUrl(string fullUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(fullUrl);
            }
        }

        /*private string ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection gameRows = htmlDoc.DocumentNode.SelectNodes("//tr[@class='next-game']");
            if (gameRows == null)
            {
                return "Schedule not found";
            }

            StringBuilder builder = new StringBuilder();
            foreach (HtmlNode row in gameRows)
            {
                // Extract date
                HtmlNode dateNode = row.SelectSingleNode(".//td[@data-label='Data']");
                if (dateNode != null)
                {
                    builder.Append($"<tr><td data-label='Data'>{dateNode.InnerHtml}</td>");
                }

                // Extract team logos
                HtmlNode teamLogos = row.SelectSingleNode(".//td[@data-label='Komandos']");
                if (teamLogos != null)
                {
                    HtmlNodeCollection logos = teamLogos.SelectNodes(".//img");
                    if (logos != null && logos.Count == 2)
                    {
                        string homeTeamLogoUrl = logos[0].GetAttributeValue("src", "");
                        string awayTeamLogoUrl = logos[1].GetAttributeValue("src", "");

                        // Insert logos with the team-logo class
                        builder.Append($"<td data-label='Komandos' class='team-logo'><img src='{homeTeamLogoUrl}' alt='Home Team Logo' /></td>");
                        builder.Append($"<td data-label='Komandos' class='team-logo'><img src='{awayTeamLogoUrl}' alt='Away Team Logo' /></td>");
                    }
                }

                // Extract other columns
                string[] headers = { "Lyga", "Transliuojama", "Vieta", "Bilietai" };
                string[] columnWidths = { "20%", "30%", "30%", "10%" }; // Adjust column widths as needed
                foreach (string header in headers)
                {
                    HtmlNode column = row.SelectSingleNode($".//td[@data-label='{header}']");
                    if (column != null)
                    {
                        builder.Append($"<td data-label='{header}' style='width: {columnWidths[Array.IndexOf(headers, header)]};'>{column.InnerHtml}</td>");
                    }
                    else
                    {
                        // Insert an empty cell if the column is missing
                        builder.Append($"<td data-label='{header}' style='width: {columnWidths[Array.IndexOf(headers, header)]};'></td>");
                    }
                }

                builder.Append("</tr>");
            }

            return builder.ToString();
        }*/
        private string ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection gameRows = htmlDoc.DocumentNode.SelectNodes("//tr[@class='next-game']");
            if (gameRows == null)
            {
                return "Schedule not found";
            }

            StringBuilder builder = new StringBuilder();
            foreach (HtmlNode row in gameRows)
            {
                // Extract date
                HtmlNode dateNode = row.SelectSingleNode(".//td[@data-label='Data']");
                if (dateNode != null)
                {
                    builder.Append($"<tr><td data-label='Data'>{dateNode.InnerHtml}</td>");
                }

                // Extract team logos
                HtmlNode teamLogos = row.SelectSingleNode(".//td[@data-label='Komandos']");
                if (teamLogos != null)
                {
                    HtmlNodeCollection logos = teamLogos.SelectNodes(".//img");
                    if (logos != null && logos.Count == 2)
                    {
                        string homeTeamLogoUrl = logos[0].GetAttributeValue("src", "");
                        string awayTeamLogoUrl = logos[1].GetAttributeValue("src", "");

                        // Insert logos in the same column
                        builder.Append($"<td data-label='Komandos' class='team-logo'><img src='{homeTeamLogoUrl}' alt='Home Team Logo' /><img src='{awayTeamLogoUrl}' alt='Away Team Logo' class='team-logo' /></td>");
                    }
                }

                // Extract other columns
                string[] headers = { "Lyga", "Transliuojama", "Vieta", "Bilietai" };
                string[] columnWidths = { "auto", "auto", "auto", "auto" }; // Adjust column widths as needed
                foreach (string header in headers)
                {
                    HtmlNode column = row.SelectSingleNode($".//td[@data-label='{header}']");
                    if (column != null)
                    {
                        builder.Append($"<td data-label='{header}' style='width: {columnWidths[Array.IndexOf(headers, header)]};'>{column.InnerHtml}</td>");
                    }
                    else
                    {
                        // Insert an empty cell if the column is missing
                        builder.Append($"<td data-label='{header}' style='width: {columnWidths[Array.IndexOf(headers, header)]};'></td>");
                    }
                }

                builder.Append("</tr>");
            }

            return builder.ToString();
        }

    }
}