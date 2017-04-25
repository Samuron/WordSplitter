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
            _inner = StringComparer.Create(CultureInfo.GetCultureInfo("de"), true);
        }

        public bool Equals(string x, string y)
        {
            return _inner.Equals(x, y) || CompareWithInterfix(x, y);
        }

        public int GetHashCode(string obj)
        {
            if (obj == null)
                return 0;
            return _inner.GetHashCode(obj.TrimEnd('s'));
        }

        private bool CompareWithInterfix(string x, string y)
        {
            if (x.EndsWith("s"))
                return _inner.Equals(x.Substring(0, x.Length - 1), y);

            if (y.EndsWith("s"))
                return _inner.Equals(x, y.Substring(0, y.Length - 1));

            return false;
        }
    }
}