using System;
using System.Collections.Generic;
using System.Text;
using JSONClasses;

namespace Http
{
    public class MethodMatch : IMatch
    {
        private readonly string remainingText;
        private readonly Method method;

        public MethodMatch(string remainingText, Method method)
        {
            this.remainingText = remainingText;
            this.method = method;            
        }

        public Method Method()
        {
            return method;
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
