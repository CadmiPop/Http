using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public class Response
    {
        private readonly int status;
        private readonly string httpVersion;
        private readonly FileInfo file;
        private string body;

        public Response(int status, FileInfo file)
        {
            httpVersion = "HTTP/1.1";
            this.status = status;
            this.file = file;
        }

        private Dictionary<int, string> statusAndMessage = new Dictionary<int, string>
        {
            {200, "OK" },
            {400, "Bad Request" },
            {404, "Not found" },
        };

        private Dictionary<string[], string> mimeTypes = new Dictionary<string[], string>()
        {
            {new string[]{"html","css","javascript","plain"}, "text" },
            {new string[]{"gif","jpeg","jpg","png","bmp"}, "image" },
            {new string[]{"wav","mpeg","ogg", "mp3"}, "audio" },
            {new string[]{"pdf","zip","xml","json" }, "application" }
        };

        private Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>(){};

        public void Body(string bodyText, string contentType = "")
        {
            body = bodyText;
            Headers.Add("Content-Length: ", bodyText.Length.ToString());
            Headers.Add("Content-Type: ", contentType =  GetContentType());
        }

        public void Body(Stream file, long contentLength, string contentType = "")
        {
            body = File.ReadAllText(file.ToString());
            Headers.Add("Content-Length: ", contentLength.ToString());
            Headers.Add("Content-Type: ", contentType = GetContentType());
        }

        public string GetContentType()
        {
            var ext = file.Extension.Substring(1);
            var type = mimeTypes[mimeTypes.Keys.First(x => x.Contains(ext))];
            return type;
        }

        public async Task Write(Stream stream)
        {
            //var streamWriter = new StreamWriter(stream)
            //{ AutoFlush = true };
            //await streamWriter.WriteAsync(GetResponse().ToString());

            await stream.WriteAsync(GetResponse());
            FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            await fileStream.CopyToAsync(stream);
        }

        private byte[] GetResponse()
        {
            string response = $"HTTP/1.1 {status} {statusAndMessage[status]}\r\n";
            foreach (var header in Headers)
                response += $"{header.Key}{header.Value.Trim()}\r\n";
            response += "\r\n";
            var a = Encoding.ASCII.GetBytes(response);
            return a;
            //var responseWithoutBody = Encoding.ASCII.GetBytes(response);
            //var fullResponse = new byte[responseWithoutBody.Length + body.Length];
            //Array.Copy(responseWithoutBody, fullResponse, responseWithoutBody.Length);
            //Array.Copy(body, 0, fullResponse, responseWithoutBody.Length, body.Length);
            //return fullResponse;
        }
    }
}
