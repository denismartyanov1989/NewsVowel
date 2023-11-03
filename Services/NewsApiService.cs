using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewsVowel.Models;
using Newtonsoft.Json;

namespace NewsVowel.Services
{
    public class NewsApiService
    {
        private const string BaseEndpoint = "https://newsapi.org/v2/";
        private const string GetEverythingCommand = "everything";
        private static readonly HttpClient Client = new HttpClient();

        static NewsApiService()
        {
            // Security protocols disabled by default on Windows 7,
            // so we need to enable them manually to support https.
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Api doesn't work without user agent.
            Client.DefaultRequestHeaders.Add("user-agent", "News-Vowel/1.0");
        }

        public NewsApiService(string apiKey)
        {
            Client.DefaultRequestHeaders.Add("x-api-key", apiKey);

        }

        public async Task<EverythingResponse> GetEverythingAsync(string query, SearchFields? searchFields, Language language)
        {
            var url = BaseEndpoint + GetEverythingCommand + "?" + this.GetGetEverythingParameters(query, searchFields, language);
            using (var httpResponse = await Client.GetAsync(url))
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    var response = JsonConvert.DeserializeObject<EverythingResponse>(json);
                    if (response.Status == Status.Error)
                    {
                        throw new HttpRequestException(
                            $"API returned error code. Status code: {httpResponse.StatusCode}, response code: {response.Code}, response message: {response.Message}");
                    }

                    return response;
                }
                else
                {
                    throw new HttpRequestException(
                        $"Response content is empty. Response status code {httpResponse.StatusCode}");
                }
            }
        }

        private string GetGetEverythingParameters(string query, SearchFields? searchFields, Language language)
        {
            var sb = new StringBuilder();
            sb.Append($"{Parameters.QueryParameter}={WebUtility.UrlEncode(query)}");
            if (searchFields.HasValue)
            {
                sb.Append($"&{Parameters.SearchFieldsParameter}={searchFields.Value.ToString("G").ToLower().Replace(" ", string.Empty)}");
            }

            sb.Append($"&{Parameters.LanguageParameter}={language.ToString("G").ToLower()}");

            return sb.ToString();
        }
    }
}