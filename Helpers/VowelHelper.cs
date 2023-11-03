using System.Collections.Generic;
using System.Linq;

namespace NewsVowel.Helpers
{
    public class VowelHelper
    {
        private static readonly HashSet<char> Vowels = new HashSet<char>
            { 'a', 'e', 'i', 'o', 'u', 'а', 'у', 'о', 'и', 'э', 'ы', 'A', 'E', 'I', 'O', 'U', 'А', 'У', 'О', 'И', 'Э', 'Ы' };

        public static string GetWordWithMaxVowels(string text)
        {
            var maxVowels = -1;
            var output = "";
            foreach (var word in text.Split(' '))
            {
                var currentVowels = word.Count(l => Vowels.Contains(l));
                if (currentVowels > maxVowels)
                {
                    maxVowels = currentVowels;
                    output = word;
                }
            }

            return output;
        }
    }
}