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
        [InlineData("arbeitsamt", new[] {"arbeitsamt"})]
        [InlineData("krakenhaus", new[] {"kraken", "haus"})]
        [InlineData("kraftfahrer", new[] {"kraft", "fahrer"})]
        [InlineData("online", new[] {"online"})]
        [InlineData("büro", new[] {"büro"})]
        [InlineData("erzieher", new[] {"erzieher"})]
        [InlineData("kraftfahrer", new[] {"kraft", "fahrer"})]
        [InlineData("psychologie", new[] {"psychologie"})]
        [InlineData("hausmeister", new[] {"haus", "meister"})]
        [InlineData("heilerziehungspfleger", new[] {"heil", "erziehungs", "pfleger"})]
        [InlineData("donaudampfschifffahrtskapitän", new[] {"donau", "dampf", "schiff", "fahrts", "kapitän"})]
        public void CanSplitWord(string word, string[] expected)
        {
            var result = _sut.Split(word);

            Assert.Equal(expected, result);
        }
    }
}