using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsVowel.Models;

namespace NewsVowel.Services
{
    public class NewsService
    {
        private readonly NewsApiService _apiService;

        public NewsService(NewsApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Article>> GetArticlesAsync(string query, SearchFields? searchFields, List<Language> languages)
        {
            var tasks = languages.Select(l => _apiService.GetEverythingAsync(query, searchFields, l));
            return (await Task.WhenAll(tasks)).SelectMany(t => t.Articles).ToList();
        }
    }
}