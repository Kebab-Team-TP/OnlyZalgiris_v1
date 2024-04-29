using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.Json;
using System.Web.Mvc;
using Zalgiris.Models;

namespace Zalgiris.ArticleScraper
{
    public class ArticleController
    {
        static string relativePath = "App_Data/Articles.json";
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string fullPath = Path.Combine(currentDirectory, relativePath);
        /// <summary>
        /// Gets all articles from JSON file containing them
        /// </summary>
        /// <returns>List of articles</returns>
        public static List<Article> GetAll()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    return JsonSerializer.Deserialize<List<Article>>(reader.ReadToEnd());
                }
            }
            catch
            {
                return new List<Article>();
            }
        }
        /// <summary>
        /// Adds a new article
        /// </summary>
        /// <param name="newArticle">Article to add</param>
        public static void Add(Article newArticle)
        {
            List<Article> articles = GetAll();
            articles.Add(newArticle);
            SaveArticles(articles);
        }
        /// <summary>
        /// Saves articles to json file
        /// </summary>
        /// <param name="articles">List of articles to save</param>
        public static void SaveArticles(List<Article> articles)
        {
            string jsonString = JsonSerializer.Serialize(articles);
            File.WriteAllText(fullPath, jsonString);
        }
        /*public static string GetJSON() 
        {
            string json = File.ReadAllText
        }*/
    }
}