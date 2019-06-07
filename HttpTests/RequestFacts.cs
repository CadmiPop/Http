using System;
using System.Collections.Generic;
using System.Text;
using Http;
using Xunit;

namespace HttpTests
{
    public class RequestFacts
    {
        [Fact]
        public void Test_Request()
        {
            var example = "GET http://www.w3.org/pub/WWW/TheProject.html HTTP/1.1\r\n" +
                       "Host: www.xyz.com\r\n" +
                       "Connection: Keep - Alive\r\n" +
                       "Accept: image / gif, image / jpeg,\r\n" +
                       "Accept-Language: us-en, fr, cn\r\n\r\n";
            var pattern = new HttpRequest();
            var match = pattern.Match(example);
            Assert.True(match.Success());
        }
    }
}
