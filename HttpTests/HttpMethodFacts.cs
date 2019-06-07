using System;
using System.Collections.Generic;
using System.Text;
using Http;
using Xunit;

namespace HttpTests
{
    public class HttpMethodFacts
    {
        [Theory]
        [InlineData("GET / HTTP 1.1", Method.GET)]
        [InlineData("POST / HTTP 1.1", Method.POST)]
        public void Returns_a_match_with_http_method_parsed(string text, Method method)
        {
            var pattern = new HttpMethod();
            var match = (MethodMatch) pattern.Match(text);
            Assert.True(match.Success());
            Assert.Equal(method, match.Method());
        }
        [Theory]
        [InlineData("GExT / HTTP 1.1")]
        public void Should_NOT_match_an_invalid_method(string text)
        {
            var pattern = new HttpMethod();
            var match = pattern.Match(text);
            Assert.False(match.Success());
        }
    }
}
