using System;
using Http;
using Xunit;

namespace HttpTests
{
    public class RequestLineFacts
    {
        [Fact]
        public void Test1()
        {
            var url = "GET http://www.w3.org/pub/WWW/TheProject.html HTTP/1.1\r\n";
            var example = new RequestLine();
            var b = example.Match(url).RemainingText();
            Assert.True(example.Match(url).Success());
        }

        [Fact]
        public void Test2()
        {
            var url = "GET /docs/index.html HTTP/1.1\r\n";
            var example = new RequestLine();
            var b = example.Match(url).RemainingText();
            Assert.True(example.Match(url).Success());
        }
    }
}
