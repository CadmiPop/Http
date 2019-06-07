using System;
using System.Collections.Generic;
using System.Text;
using JSONClasses;

namespace Http
{
    public class RequestHeader : IPattern
    {
        private IPattern pattern;

        public RequestHeader()
        {
            var alpha = new Choice(new Range('a', 'z'), new Range('A', 'Z'));
            var digit = new Range('0', '9');
            var specialChars = new Any(" ()[]}<>{.-;/?:@&=+$\",");
            var lineEnd = new Text("\r\n");
            this.pattern = new Sequance(new Many(new Choice(alpha, digit, specialChars)), lineEnd);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}

