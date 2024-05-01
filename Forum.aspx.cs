using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zalgiris.Models;
using HtmlAgilityPack; 
using System.Net.Http;
using System.Threading.Tasks;


namespace Zalgiris
{
    public partial class Forum : System.Web.UI.Page
    {
        static string relativePath = "App_Data/Forum.json";
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string fullPath = Path.Combine(currentDirectory, relativePath);
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/Login", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!IsPostBack)
            {
                await PopulateMatchDates();
            }
            try
            {
                List<Message> messages = ReadMessages();
                if (DropDownList1.SelectedValue != "All" && DropDownList1.SelectedValue != null)
                {
                    messages = messages.Where(m => DateTime.Parse(m.datetime).ToString("yyyy-MM-dd") == DropDownList1.SelectedValue).ToList();
                }
                FillBlock(messages);
            }
            catch (Exception ex)
            {
                MessagesField.Text = ex.Message;
            }
        }


        protected void Submit()
        {
            
        }
        private List<Message> ReadMessages()
        {
            using (StreamReader reader = new StreamReader(fullPath))
            {
                return JsonSerializer.Deserialize<List<Message>>(reader.ReadToEnd());
            }
        }

        protected void MessageText_TextChanged(object sender, EventArgs e)
        {
        }
        private DateTime ParseLithuanianDate(string dateStr)
        {
            var monthNames = new Dictionary<string, int>
    {
        {"Sausio", 1}, {"Vasario", 2}, {"Kovo", 3},
        {"Balandžio", 4}, {"Gegužės", 5}, {"Birželio", 6},
        {"Liepos", 7}, {"Rugpjūčio", 8}, {"Rugsėjo", 9},
        {"Spalio", 10}, {"Lapkričio", 11}, {"Gruodžio", 12}
    };

            // Split the input string into parts
            var parts = dateStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3) return DateTime.MinValue;

            // Extract year, month, and day
            int year = int.Parse(parts[0]);
            int month = monthNames[parts[1]];
            int day = int.Parse(parts[2].TrimEnd('d', '.'));

            // Construct a DateTime object
            return new DateTime(year, month, day);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                string user = Session["UserName"] != null ? (string)Session["UserName"] : "";
                List<Message> Messages = ReadMessages();
                Messages.Add(new Message(MessageText.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), user));
                string jsonString = JsonSerializer.Serialize(Messages);
                File.WriteAllText(fullPath, jsonString);
                Response.Redirect("Forum");
            }
            catch (Exception ex)
            {
                MessagesField.Text = ex.Message;
            }
        }
        protected void FillBlock(List<Message> Messages)
        {
            StringBuilder MessagesHTML = new StringBuilder();
            string user = Session["UserName"] != null ? (string)Session["UserName"] : "";

            foreach (Message message in Messages)
            {
                string MessageBlock = String.Format("<div class='message-block {2}'><span class='date'>{1}</span><p class='text'>{0}</p></div>", message.text, message.datetime, message.author == user ? "author":"");
                MessagesHTML.Append(MessageBlock);
            }
            MessagesField.Text = MessagesHTML.ToString();
        }




        private async Task PopulateMatchDates()
        {
            var matchDates = await FetchMatchDates();
            matchDates.Insert(0, ("Visos žinutės", "All")); // Adds "All Dates" as the first option in the dropdown
            DropDownList1.DataSource = matchDates.Select(m => m.display).ToList();
            DropDownList1.DataBind();

            // Setting the Value property for filtering purposes
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.Items[i].Value = matchDates[i].date;
            }
        }



        private async Task<List<(string display, string date)>> FetchMatchDates()
        {
            string url = "https://zalgiris.lt/schedule/bc-zalgiris/";
            using (HttpClient client = new HttpClient())
            {
                string html = await client.GetStringAsync(url);
                return ParseMatchDates(html);
            }
        }

        private List<(string display, string date)> ParseMatchDates(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNodeCollection gameRows = htmlDoc.DocumentNode.SelectNodes("//tr[contains(@class, 'next-game') or contains(@class, 'prev-game')]");
            List<(string display, string date)> matches = new List<(string display, string date)>();
            if (gameRows != null)
            {
                foreach (HtmlNode row in gameRows)
                {
                    var dateNode = row.SelectSingleNode(".//td[@data-label='Data']").InnerText.Trim();
                    var dateOnly = ParseLithuanianDate(dateNode).ToString("yyyy-MM-dd"); // Ensures the date is in a standard format
                    var teamsNode = row.SelectSingleNode(".//td[@data-label='Komandos']");
                    var teams = teamsNode.SelectNodes(".//div[@class='team-name']").Select(node => node.InnerText.Trim()).ToList();

                    int zalgirisIndex = teams.IndexOf("Kauno „Žalgiris“");
                    string opponent = (zalgirisIndex == 0 && teams.Count > 1) ? teams[1] : teams[0];
                    string matchInfo = $"{dateNode} vs. {opponent}";

                    matches.Add((matchInfo, dateOnly));
                }
            }
            return matches;
        }





        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page_Load(sender, e); // This will re-trigger the Page_Load with the new filter applied.
        }



    }

}