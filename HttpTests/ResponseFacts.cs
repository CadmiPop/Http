using Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
namespace HttpTests
{
    public class ResponseFacts
    {
        [Fact]
        public void Test_Write_string_format()
        {
            // given
            var stream = new MemoryStream();
            var response = new Response(200);
            // when
            response.Write(stream);
            // then
            var expected = "HTTP/1.1 200 OK\r\n";
            stream.Seek(0, SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Write_File_format()
        {
            // given
            var stream = new MemoryStream();
            var response = new Response(200);            
            // when
            response.Write(stream);
            // then
            var expected = "HTTP/1.1 200 OK\r\n";
            stream.Seek(0, SeekOrigin.Begin);
            var result =  new StreamReader(stream).ReadToEnd();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Get_type()
        {
            var response = new Response(200);

            string b = response.GetContentType();
        }


    }
}
