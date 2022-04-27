﻿using System;
using System.Net;
using System.Threading.Tasks;
using News.Models;
using Xamarin.Forms;
using System.Text.Json;

namespace News.Services
{
    public class NewsService
    {
        public async Task<NewsResult> GetNews(NewsScope scope)
        {
            string url = GetUrl(scope);

            var webclient = new WebClient();
            var json = await webclient.DownloadStringTaskAsync(url);

            JsonSerializerOptions options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true 
            };

            return JsonSerializer.Deserialize<NewsResult>(json, options);
            //Newtonsoft.Json.JsonConvert.DeserializeObject<NewsResult>(json);
        }

        private string GetUrl(NewsScope scope)
        {
            return scope switch
            {
                NewsScope.Headlines => Headlines,
                NewsScope.Global => Global,
                NewsScope.Local => Local,
                _ => throw new Exception("Undefined scope")
            };
        }

        private string Headlines => "https://newsapi.org/v2/top-headlines?" +
                                    "country=nl&" +
                                    $"apiKey={Settings.NewsApiKey}";

        private string Local => "https://newsapi.org/v2/everything?q=breda&language=nl&" +
                                $"apiKey={Settings.NewsApiKey}";

        private string Global => "https://newsapi.org/v2/everything?q=nederland&language=nl&" +
                                 $"apiKey={Settings.NewsApiKey}";
    }
}