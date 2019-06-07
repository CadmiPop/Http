using System;
using System.Collections.Generic;
using System.Text;
using Http;
using Xunit;

namespace HttpTests
{
    public class HttpHeaderFacts
    {
        [Fact]
        public void Returns_a_Dictionary_of_Headers()
        {
            var text = "Host: www.xyz.com\r\n" +
                       "Connection: Keep - Alive\r\n" +
                       "Accept: image / gif, image / jpeg,\r\n" +
                       "Accept-Language: us-en, fr, cn\r\n\r\n";
            var dic =  new Dictionary<string,string>
            {
                {"Host" , "www.xyz.com"} ,
                {"Connection" , "Keep - Alive"} ,
                {"Accept" , "image / gif, image / jpeg"} ,
                {"Accept-Language" , "us-en, fr, cn"} ,

            };
            var pattern = new HttpHeader();
            var match = pattern.Match(text);
            Assert.True(match.Success());
        }
    }
}
