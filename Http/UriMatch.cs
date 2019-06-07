using System;
using System.Collections.Generic;
using System.Text;
using JSONClasses;

namespace Http
{
    public class UriMatch : IMatch
    {
        private readonly string remainingText;
        private readonly Uri uri;

        public UriMatch(string remainingText, Uri uri)
        {
            this.remainingText = remainingText;
            this.uri = uri;
        }

        public Uri Uri()
        {
            return uri;
        }

        public bool Success()
        {
            return true;
        }

        public string RemainingText()
        {
            return remainingText;
        }
    }
}
