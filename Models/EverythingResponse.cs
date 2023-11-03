using System.Collections.Generic;

namespace NewsVowel.Models
{
    public class EverythingResponse
    {
        public Status Status { get; set; }

        public int TotalResults { get; set; }

        public List<Article> Articles { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}