﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zalgiris.ArticleScraper
{
    public class Article
    {
        public string Description { get; set; } // Short description about the article
        public string ImageUrl { get; set; }
        public string ArticleUrl { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }

        public Article(string description, string imageUrl, string articleUrl)
        {
            Description = description;
            this.ImageUrl = imageUrl;
            this.ArticleUrl = articleUrl;
        }

        public Article(string description, string imageUrl, string articleUrl, string date) : this(description, imageUrl, articleUrl)
        {
            Date = date;
        }

        public Article(string description, string imageUrl, string articleUrl, string date, string name) : this(description, imageUrl, articleUrl, date)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"Article Name: {Name}\nDescription: {Description}\nImage URL: {ImageUrl}\nArticle URL: {ArticleUrl}\nDate: {Date}";
        }
    }
}