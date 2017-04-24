using System;
using System.Collections.Generic;
using System.Globalization;

namespace FunTimeWithWords
{
    internal class GermanWordsComparer : IEqualityComparer<string>
    {
        private readonly StringComparer _inner;

        public GermanWordsComparer()
        {
            var cultureInfo = CultureInfo.GetCultureInfo("de");
            _inner = StringComparer.Create(cultureInfo, true);
        }

        public bool Equals(string x, string y)
        {
            return _inner.Equals(x, y);
        }

        public int GetHashCode(string obj)
        {
            return _inner.GetHashCode(obj);
        }
    }
}