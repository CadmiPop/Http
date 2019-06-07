using System;
using System.Collections.Generic;
using System.Text;
using JSONClasses;

namespace Http
{
    public class RequestLine : IPattern
    {
        private IPattern pattern;

        public RequestLine()
        {
            var token = new OneOrMore(new Choice(
                new Character('!'),
                new Range('#','\''),
                new Range('*','+'),
                new Range('-','.'),
                new Range('^','`'),
                new Range('0','9'),
                new Range('a','z'),
                new Range('A','Z'),
                new Character('|'),
                new Character('~')));

            var digits = new OneOrMore(new Range('0','9'));
            var extensionMethod = token;

            var method = new Choice(
                new Text("OPTIONAL"),
                new Text("GET"),
                new Text("HEAD"),
                new Text("POST"),
                new Text("PUT"),
                new Text("DELETE"),
                new Text("CONNECT"),
                new Text("TRACE"));

            var slash = new Character('/');
            var period = new Character('.');
            var space = new Character(' ');
            var lineEnd = new Text("\r\n");
            var httpVersion = new Sequance(new Text("HTTP"),slash,digits,period,digits);

            this.pattern = new Sequance(method, space, new RequestURI(), space, httpVersion, lineEnd);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
