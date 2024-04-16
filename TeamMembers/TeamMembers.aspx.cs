using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace Zalgiris.TeamMembers
{
    public partial class TeamMembers : System.Web.UI.Page
    {
        protected List<Staff> staffList = new List<Staff>();
        protected List<Player> playerList = new List<Player>();
        protected Member currentMember = null;
        protected string currentMemberURL = "";
        protected string Statistics= "";
        protected async void Page_Load(object sender, EventArgs e)
        {
            bool showMember = false;
            // Check if it's a postback to prevent reloading on every postback
            if (!string.IsNullOrEmpty(Request.QueryString["member"]))
            {
                 showMember = true;
                // Retrieve the value of the 'member' parameter
                string member = Request.QueryString["member"];
                //Console.WriteLine("Member parameter value: " + member);
                
                if (!IsPostBack)
                {
                    await FindMember(member);

                }
            }
            else if(!IsPostBack)
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
        public static string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        private async Task FindMember(string member)
        {
            string url = $"https://zalgiris.lt/team/bc-zalgiris/{member}/";
            string html = await CallUrl(url);
            ParseMemberHtml(html);
            
            try
            {
                url = $"https://www.basketnews.lt/komandos/265-kauno-zalgiris.html";
                html = await CallUrl(url);
                currentMemberURL = $"https://www.basketnews.lt{FindUrl(html)}";
                html = await CallUrl(currentMemberURL);
                Statistics = currentMemberURL;
                FindStatistics(html);
            }
            catch {

            }
        }
        private void ParseMemberHtml(string html) {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode doc = htmlDoc.DocumentNode;
            HtmlNode photo = doc.SelectSingleNode("//div[@class='columns small-12 medium-6 photo text-center']/img");
            HtmlNode shield = doc.SelectSingleNode("//div[@class='shield']");
            HtmlNode positionNode = doc.SelectSingleNode("//h2[@class='name text-left']/span");

            if (photo == null) return;
            
            string imgLink = photo.GetAttributeValue("img-lazy", "no link");
            string Name = photo.GetAttributeValue("alt", "no name");
            if (shield == null)
                currentMember = new Staff(Name, positionNode.InnerHtml, imgLink);
            else
            {
                Player currentPlayer = new Player(shield.InnerHtml, Name, positionNode.InnerHtml, imgLink);
                currentMember = currentPlayer;
            }
            try {
                string Age = doc.SelectSingleNode("//div[@class='stats last']/span[@class='meaning']").InnerHtml;
                currentMember.Age = Age;
            }catch { }
            try{
                string Height = doc.SelectSingleNode("//div[@class='stats small']/span[@class='meaning']").InnerHtml;
                currentMember.Height = Height;
            }catch { }
            try{
                string Weight = doc.SelectSingleNode("//div[@class='stats medium']/span[@class='meaning']").InnerHtml;
                currentMember.Weigth = Weight;
            }catch { }
            currentMember.Info = doc.SelectSingleNode("//div[@class='columns small-12 content margin-bottom-25'] ").InnerHtml;
            
        }
        private string FindUrl(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode doc = htmlDoc.DocumentNode;
            HtmlNode linkToPage = doc.SelectSingleNode($"//td[@class=\"name\"]/a[contains(@href,\"{currentMember.Slug}\")]");
            return linkToPage.Attributes["href"].Value;    
        }
        private void FindStatistics(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode doc = htmlDoc.DocumentNode;
            HtmlNodeCollection stats = doc.SelectNodes("//div[@class='main_stats']/div");
            string category;
            string value;
            string position;
            StringBuilder builder = new StringBuilder();

            category = "Taškai";
            value = stats[0].SelectSingleNode("//div[@class='value']").InnerHtml;
            position = stats[0].SelectSingleNode("//div[@class='position']/a").InnerHtml;
            builder.AppendLine($"<div class='card stat'><div>{category}</div><div class='number'>{value}</div><div>{position}</div></div>");
            
            category = "Atkovoti kamuoliai";
            value = stats[1].SelectSingleNode("./div[@class='value']").InnerHtml;
            position = stats[1].SelectSingleNode("./div[@class='position']/a").InnerHtml;
            builder.AppendLine($"<div class='card stat'><div>{category}</div><div class='number'>{value}</div><div>{position}</div></div>");
            
            category = "Rez. perdavimai";
            value = stats[2].SelectSingleNode("./div[@class='value']").InnerHtml;
            position = stats[2].SelectSingleNode("./div[@class='position']/a").InnerHtml;
            builder.AppendLine($"<div class='card stat'><div>{category}</div><div class='number'>{value}</div><div>{position}</div></div>");
            
            category = "Efektyvumo balas";
            value = stats[3].SelectSingleNode("./div[@class='value']").InnerHtml;
            position = stats[3].SelectSingleNode("./div[@class='position']/a").InnerHtml;
            builder.AppendLine($"<div class='card stat'><div>{category}</div><div class='number'>{value}</div><div>{position}</div></div>");
            Statistics = builder.ToString();
        }
    }
}