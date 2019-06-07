using System;
using System.Collections.Generic;
using System.Text;
using JSONClasses;

namespace Http
{
    public class Request
    { 
        public Method Method { get; set; }
        public Uri Url { get; set; }
        public Dictionary<string,string> Header { get; set; }
    }
}
