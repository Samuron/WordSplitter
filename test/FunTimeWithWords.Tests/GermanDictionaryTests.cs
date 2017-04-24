using Xunit;

namespace FunTimeWithWords.Tests
{
    public class GermanDictionaryTests
    {
        [Fact]
        public void CanGetDefaultDictionary()
        {
            var dictionary = GermanDictionary.Default;

            Assert.NotEmpty(dictionary);
        }
    }
}