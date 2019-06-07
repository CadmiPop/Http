using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSONClasses;

namespace Http
{
    public class RequestMatch : IMatch
    {
        private string remainingText;

        public RequestMatch(string remainingText, List<IMatch> matches)
        {
            this.remainingText = remainingText;
            Request.Method = ((MethodMatch)matches.First(m => m is MethodMatch)).Method();
            Request.Url = ((UriMatch)matches.First(m => m is UriMatch)).Uri();
            Request.Header = ((HeaderMatch)matches.First(m => m is HeaderMatch)).Headers();
        }

        public Request Request { get; private set; } = new Request();

        public string RemainingText() => remainingText;

        public bool Success() => true;
    }
}
