using Http;
using System;
using Xunit;

namespace HttpTests
{
    public class RequestURIFacts
    {
        [Fact]
        public void Test1()
        {
            var URI = "a";
            var example = new RequestURI();
            var b = example.Match(URI).RemainingText();
            Assert.True(example.Match(URI).Success());
        }

        [Fact]
        public void Test2()
        {
            var URI = "http://server";
            var example = new RequestURI();
            var b = example.Match(URI).RemainingText();
            Assert.True(example.Match(URI).Success());
        }

        [Fact]
        public void Test3()
        {
            var URI = "magnet://server:8088/func1/SubFunc1";
            var example = new RequestURI();
            var b = example.Match(URI).RemainingText();
            Assert.True(example.Match(URI).Success());
        }
        
    }
}
