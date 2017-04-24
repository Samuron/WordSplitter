using Xunit;

namespace FunTimeWithWords.Tests
{
    public class GermanWordsComparerTests
    {
        private readonly GermanWordSplitter _sut;

        public GermanWordsComparerTests()
        {
            _sut = new GermanWordSplitter(GermanDictionary.Default, 3);
        }

        [Theory]
        [InlineData("krakenhaus", new[] {"kraken", "haus"})]
        [InlineData("psychologie", new[] {"psychologie"})]
        [InlineData("hausmeister", new[] {"haus", "meister"})]
        public void CanSplitWord(string word, string[] expected)
        {
            var result = _sut.Split(word);

            Assert.Equal(expected, result);
        }
    }
}