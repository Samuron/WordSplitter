using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace FunTimeWithWords.Tests
{
    public class PerformanceTests : IClassFixture<PerformanceTestsFixture>
    {
        private readonly ITestOutputHelper _helper;
        private readonly Stopwatch _stopwatch;
        private readonly GermanWordSplitter _sut;
        private readonly TimeSpan _target;

        public PerformanceTests(PerformanceTestsFixture fixture, ITestOutputHelper helper)
        {
            _helper = helper;
            _sut = fixture.Splitter;
            _target = fixture.Target;
            _stopwatch = fixture.Stopwatch;
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

            _helper.WriteLine($"Split {word} in {_stopwatch.Elapsed:c}");

            Assert.True(_stopwatch.Elapsed <= _target, $"Could not split {word} in time. Took {_stopwatch.Elapsed:c}");
        }
    }

    public class PerformanceTestsFixture
    {
        public PerformanceTestsFixture()
        {
            Stopwatch = new Stopwatch();
            Splitter = new GermanWordSplitter(GermanDictionary.Default, 3);
            Stopwatch.Start();
            for (var i = 0; i < 100000; i++)
            {
                Splitter.Split("airbus");
                Splitter.Split("krakenhaus");
                Splitter.Split("volksbank");
                Splitter.Split("weihnachtsmarkt");
            }
            Stopwatch.Stop();
            Target = TimeSpan.FromTicks(TimeSpan.TicksPerMillisecond / 5); // 0.2 ms
        }

        public TimeSpan Target { get; }

        public GermanWordSplitter Splitter { get; }

        public Stopwatch Stopwatch { get; }
    }
}