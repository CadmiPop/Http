using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSONClasses;

namespace Http
{
    public class HeaderMatch : IMatch
    {
        private readonly string remainingText;
        public KeyValuePair<string,string> header;
        private Dictionary<string, string> headerDictionary;

        public HeaderMatch(string remainingText, KeyValuePair<string,string> header)
        {
            this.remainingText = remainingText;
            this.header = header;
        }

        public HeaderMatch(string remmRemainingText, Dictionary<string,string> headerDictionary) 
        {
            this.headerDictionary = headerDictionary;
        }

        public Dictionary<string, string> Headers()
        {
            return headerDictionary;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public bool Success()
        {
            return true;
        }
    }
}
