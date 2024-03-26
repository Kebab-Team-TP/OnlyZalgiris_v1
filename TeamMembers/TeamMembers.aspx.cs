using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zalgiris.TeamMembers
{
    public partial class TeamMembers : System.Web.UI.Page
    {
        protected List<Staff> staffList = new List<Staff>();
        protected List<Player> playerList = new List<Player>();
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if it's a postback to prevent reloading on every postback
            if (!IsPostBack)
            {
                await FindTeamMembers();
            }
        }
        private async Task FindTeamMembers()
        {
            string url = "https://zalgiris.lt/team/bc-zalgiris/";
            string html = await CallUrl(url);
            ParseHtml(html);
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
            // Get staff
            var staffElements = htmlDoc.DocumentNode.SelectNodes("//li[@class='item staff']");
            // Get players
            var playerElements = htmlDoc.DocumentNode.SelectNodes("//li[@class='item']");
            foreach (var staff in staffElements)
            {
                var personDiv = staff.SelectSingleNode(".//div[@class='person']");
                var imgNode = personDiv?.SelectSingleNode(".//img");
                var nameDiv = staff.SelectSingleNode(".//div[@class='name']");
                var positionDiv = staff.SelectSingleNode(".//div[@class='position']");

                // Extract information
                string imgLink = imgNode?.GetAttributeValue("img-lazy", "");
                string name = nameDiv?.InnerText.Trim();
                string position = positionDiv?.InnerText.Trim();
                Staff newStaff = new Staff(name, position, imgLink);
                staffList.Add(newStaff);
            }
            foreach (var player in playerElements)
            {
                var personDiv = player.SelectSingleNode(".//div[@class='person']");
                var shieldDiv = personDiv?.SelectSingleNode(".//div[contains(@class, 'shield')]");
                var imgNode = personDiv?.SelectSingleNode(".//img");
                var nameDiv = player.SelectSingleNode(".//div[@class='name']");
                var positionDiv = player.SelectSingleNode(".//div[@class='position']");

                // Extract information
                string number = shieldDiv?.InnerText.Trim();
                string imgLink = imgNode?.GetAttributeValue("img-lazy", "");
                string name = nameDiv?.InnerText.Trim();
                string position = positionDiv?.InnerText.Trim();
                Player newPlayer = new Player(number, name, position, imgLink);
                playerList.Add(newPlayer);
            }
        }
    }
}