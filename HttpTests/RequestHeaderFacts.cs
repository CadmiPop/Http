using System;
using System.Collections.Generic;
using System.Text;
using Http;
using Xunit;

namespace HttpTests
{
    public class RequestHeaderFacts
    {

        [Fact]
        public void Test2()
        {
            var URI = "Host: www.xyz.com\r\n" +
                      "Connection: Keep - Alive\r\n" +
                      "Accept: image / gif, image / jpeg,\r\n" +
                      "Accept-Language: us-en, fr, cn";
            var example = new RequestHeader();
            var b = example.Match(URI).RemainingText();
            Assert.True(example.Match(URI).Success());
        }
    }    
}
