using NewsVowel.Helpers;
using NUnit.Framework;

namespace NewsVowel.Tests
{
    public class VowelsHelperTest
    {
        [TestCase("test aaa b", "aaa")]
        [TestCase("test AEY b", "AEY")]
        [TestCase("тест рус ауоиэы", "ауоиэы")]
        [TestCase("", "")]
        [TestCase("The Fed's favorite inflation measure came in as expected, with prices up 3.7% in September year over year.", "favorite")]
        public void GetWordWithMaxVowelsTest(string input, string expected)
        {
            Assert.AreEqual(expected, VowelHelper.GetWordWithMaxVowels(input));
        }
    }
}