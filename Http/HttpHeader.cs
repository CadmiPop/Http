using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JSONClasses;

namespace Http
{
    public class HttpHeader : IPattern
    {
        private readonly IPattern pattern;
 
        public HttpHeader()
        {
            var alpha = new Choice(new Range('a', 'z'), new Range('A', 'Z'));
            var digit = new Range('0', '9');
            var specialChars = new Any(" ()[]}<>{.-;/?:@&=+$\",");
            var lineEnd = new Text("\r\n");
            this.pattern = new Sequance(new Many(new Choice(alpha, digit, specialChars)), lineEnd);
        }
        
        public IMatch Match(string text)
        {
            var remText = text;
            IMatch match = pattern.Match(remText);
            var Headers = new Dictionary<string,string>();
            while (remText != "\r\n")
            {
                var head = new HeaderMatch(match.RemainingText(), ExtractHeaders(remText, match.RemainingText())).header;
                Headers.Add(head.Key, head.Value);
                remText = match.RemainingText();
                match = pattern.Match(remText);
            }

            return new HeaderMatch(match.RemainingText(), Headers);
        }

        public KeyValuePair<string,string> ExtractHeaders (string text, string remainingText)
        {
            var header = text.Remove(text.Length - remainingText.Length).Split(':');
            return new KeyValuePair<string, string>(header[0],header[1]);
        }
    }
}
