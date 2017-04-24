using System;
using System.Collections.Generic;
using System.Linq;

namespace FunTimeWithWords
{
    public class GermanWordSplitter
    {
        private readonly int _minimumLength;
        private readonly HashSet<string> _words;

        public GermanWordSplitter(IEnumerable<string> words, int minimumLength)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            _minimumLength = minimumLength;
            _words = words.Where(x => x.Length >= _minimumLength).ToHashSet(new GermanWordsComparer());
        }

        public List<string> Split(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(word));

            var result = SplitInternal(word, new List<string>());
            if (result.Count == 0)
                result.Add(word);

            return result;
        }

        private List<string> SplitInternal(string word, List<string> result)
        {
            if (word.Length < _minimumLength)
                return result;

            if (_words.Contains(word))
            {
                result.Add(word);
                return result;
            }

            for (var i = _minimumLength; i < word.Length; i++)
            {
                var left = word.Substring(0, i);

                if (!_words.Contains(left))
                    continue;

                var right = word.Substring(i, word.Length - i);

                var split = SplitInternal(right, new List<string>());
                if (split.Count > 0)
                {
                    result.Add(left);
                    result.AddRange(split);
                    return result;
                }
            }

            return result;
        }
    }
}