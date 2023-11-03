using System;

namespace NewsVowel.Models
{
    [Flags]
    public enum SearchFields
    {
        Title = 1,

        Description = 2,

        Content = 4
    }
}