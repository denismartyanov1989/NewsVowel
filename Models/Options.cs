using CommandLine;

namespace NewsVowel.Models
{
    public class Options
    {
        [Option("apikey", Required = true, HelpText = "Api Key for newsapi.")]
        public string ApiKey { get; set; }

        [Option('q', Default = "космос", HelpText = "Query to search for.")]
        public string Query { get; set; }

        [Option("searchfields", HelpText = "Comma separated fields to search. Possible values are: title, description, content")]
        public string SearchFields { get; set; }
    }
}