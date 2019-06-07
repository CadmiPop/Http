using JSONClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Http
{
    public class HttpRequest : IPattern
    {
        private IPattern pattern;

        public HttpRequest()
        {
            var method = new HttpMethod();
            var url = new HttpUri();
            var header = new HttpHeader();
            pattern = new Sequance(method, new Character(' '), url, new Character(' '), new Text("HTTP/1.1\r\n"), header);
        }

        public IMatch Match(string text)
        {            
            IMatch match = pattern.Match(text);
            if (match.Success())
            {
                var sequanceMatch = (SequanceMatch) match;
                return new RequestMatch(match.RemainingText(), sequanceMatch.matches);
            }
            return match;
        }
    }
}
