using JSONClasses;
using System;

namespace Http
{
    public class HttpMethod : IPattern
    {
        private readonly IPattern pattern;

        public HttpMethod()
        {
            pattern = new Choice(
               new Text("OPTIONAL"),
               new Text("GET"),
               new Text("HEAD"),
               new Text("POST"),
               new Text("PUT"),
               new Text("DELETE"),
               new Text("CONNECT"),
               new Text("TRACE"));
        }

        public IMatch Match(string text)
        {
            IMatch match = pattern.Match(text);
            return match.Success()
                ? new MethodMatch(match.RemainingText(), ExtractMethod(text, match.RemainingText()))
                : match;
        }

        private Method ExtractMethod(string text, string remainingText)
        {
            return Enum.Parse<Method>(text.Remove(text.Length-remainingText.Length));
        }
    }
}
