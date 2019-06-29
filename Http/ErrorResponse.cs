using System;
using System.Collections.Generic;
using System.Text;

namespace Http
{
    public class ErrorResponse
    {
        private readonly int statusCode;

        private Dictionary<int, string> pairs = new Dictionary<int, string>
        {
            {200, "OK" },
            {400, "Bad Request" },
            {404, "Not found" },
        };

        public ErrorResponse(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public byte[] AsByteArray()
        {
            var errorMessage = $"{statusCode} {pairs[statusCode]}";
            return Encoding.ASCII.GetBytes($"HTTP/1.1 {errorMessage}\r\nContent-Length: {errorMessage.Length}" +
            $"\r\n\r\n{errorMessage}");
        }
    }
}
