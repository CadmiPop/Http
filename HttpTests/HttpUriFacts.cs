using System;
using System.Collections.Generic;
using System.Text;
using Http;
using Xunit;

namespace HttpTests
{
    public class HttpUriFacts
    {
        [Theory]
        [InlineData("http://www.w3.org/pub/WWW/TheProject.html HTTP/1.1\r\n", "http://www.w3.org/pub/WWW/TheProject.html")]
        [InlineData("/docs/index.html HTTP/1.1\r\n", "/docs/index.html")]
        public void Returns_a_match_with_http_Uri(string text, string uri)
        {
            var pattern = new HttpUri();
            var match = (UriMatch)pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(uri, match.Uri().ToString());
        }
        [Theory]
        [InlineData("/docs/i  £$£@@@@ndex.html HTTP/1.1\r\n")]
        public void Fail_match(string text)
        {
            var pattern = new HttpUri();
            var match = pattern.Match(text);
            Assert.True(match.Success());
        }

    }
}
