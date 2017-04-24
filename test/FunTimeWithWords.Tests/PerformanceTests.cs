using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace FunTimeWithWords.Tests
{
    public class PerformanceTests : IClassFixture<PerformanceTestsFixture>
    {
        private readonly Stopwatch _stopwatch;
        private readonly GermanWordSplitter _sut;
        private readonly TimeSpan _target;

        public PerformanceTests(PerformanceTestsFixture fixture)
        {
            _sut = new GermanWordSplitter(GermanDictionary.Default, 3);
            _target = TimeSpan.FromTicks(TimeSpan.TicksPerMillisecond / 5);
            _stopwatch = new Stopwatch();
        }

        public static string[][] Cases()
        {
            var assembly = typeof(PerformanceTests).Assembly;
            using (var stream = assembly.GetManifestResourceStream("FunTimeWithWords.Tests.de-test-words.txt"))
            using (var reader = new StreamReader(stream))
            {
                var text = reader.ReadToEnd();
                var lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                return lines.Skip(1).Select(x => x.Split()).ToArray();
            }
        }

        [Theory]
        [MemberData(nameof(Cases))]
        public void CanSplitInTime(string language, string word)
        {
            _stopwatch.Restart();
            _sut.Split(word);
            _stopwatch.Stop();

            Assert.True(_stopwatch.Elapsed <= _target, $"Could not split {word} in time. Took {_stopwatch.Elapsed:c}");
        }
    }

    public class PerformanceTestsFixture
    {
        public PerformanceTestsFixture()
        {
            var splitter = new GermanWordSplitter(GermanDictionary.Default, 3);
            for (var i = 0; i < 100000; i++)
            {
                splitter.Split("krakenhaus");
                splitter.Split("volksbank");
                splitter.Split("weihnachtsmarkt");
            }
        }
    }
}