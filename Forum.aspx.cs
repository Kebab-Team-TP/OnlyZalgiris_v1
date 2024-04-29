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

namespace Zalgiris
{
    public partial class Forum : System.Web.UI.Page
    {
        static string relativePath = "App_Data/Forum.json";
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string fullPath = Path.Combine(currentDirectory, relativePath);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/Login");
            }
            try
            {
                
                List<Message> Messages = ReadMessages();
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    Messages = JsonSerializer.Deserialize<List<Message>>(reader.ReadToEnd());
                }
                FillBlock(Messages);
            }
            catch(Exception ex)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                string user = Session["UserName"] != null ? (string)Session["UserName"] : "";
                List<Message> Messages = ReadMessages();
                Messages.Add(new Message(MessageText.Text, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), user));
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
    }

}