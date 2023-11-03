using System;
using System.Collections.Generic;
using System.Linq;
using NewsVowel.Helpers;
using NewsVowel.Models;
using NewsVowel.Services;

namespace NewsVowel
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = CommandLine.Parser.Default.ParseArguments<Options>(args);
            if (options.Errors.Any())
            {
                Environment.Exit(0);
            }

            var service = new NewsService(new NewsApiService(options.Value.ApiKey));
            List<Article> articles = null;

            var fields = !string.IsNullOrEmpty(options.Value.SearchFields)
                ? (SearchFields?)Enum.Parse(typeof(SearchFields), options.Value.SearchFields)
                : null;
            try
            {
                articles = service.GetArticlesAsync(options.Value.Query, fields, new List<Language> { Language.En, Language.Ru }).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (articles.Count > 0)
            {
                foreach (var article in articles)
                {
                    Console.WriteLine($"News snippet: {article.Description}");
                    Console.WriteLine($"Word with max vowels: {VowelHelper.GetWordWithMaxVowels(article.Description)}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No results found.");
            }

            Console.ReadKey();
        }
    }
}
