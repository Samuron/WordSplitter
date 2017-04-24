using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FunTimeWithWords
{
    public class GermanDictionary : IEnumerable<string>
    {
        private static readonly Lazy<GermanDictionary> Dictionary = new Lazy<GermanDictionary>(GetEmbedded);

        private readonly IEnumerable<string> _inner;

        private GermanDictionary(IEnumerable<string> inner)
        {
            _inner = inner;
        }

        public static GermanDictionary Default => Dictionary.Value;

        public IEnumerator<string> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _inner).GetEnumerator();
        }

        private static GermanDictionary GetEmbedded()
        {
            var assembly = typeof(GermanWordSplitter).Assembly;
            using (var resources = assembly.GetManifestResourceStream("FunTimeWithWords.dict.txt"))
            using (var reader = new StreamReader(resources))
            {
                var content = reader.ReadToEnd();
                var lines = content.Split();
                return new GermanDictionary(lines);
            }
        }
    }
}