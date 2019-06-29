using JSONClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Http
{
    public class ServerHTTP
    {
        private const string defaultFileName = "/2.jpg";
        private DirectoryInfo directory;
        private TcpListener server = new TcpListener(IPAddress.Any, 80);
        private TcpClient client;
        private string path;
        private NetworkStream stream;

        public ServerHTTP(string path)
        {
            this.path = path;
            this.directory = new DirectoryInfo(path);
            Listen();
            Stop();
        }

        private void Listen()
        {
            server.Start();
            while (true)
            {
                AcceptClient();
            } 
        }

        private  void AcceptClient()
        {            
               client = server.AcceptTcpClient();
               ReadFromClient(client);           
        }

        private async Task ReadFromClient(TcpClient client)
        {
            stream = client.GetStream();
            byte[] message = new byte[5000];
            int bytesRead = await stream.ReadAsync(message, 0, message.Length);
            Array.Resize(ref message,bytesRead);
            string ex = Encoding.ASCII.GetString(message);
            Console.WriteLine(ex);

            IMatch match = new HttpRequest().Match(ex);
            if (match is RequestMatch r)
            {
                InterpretRequest(r.Request);
            }       
            else
                await stream.WriteAsync(new ErrorResponse(400).AsByteArray());
        }

        private async void InterpretRequest(Request request)
        {
            var path = request.Url.AbsolutePath;
            var file = new FileInfo(directory.FullName + path);

            if (file.Exists)
            {
                var r = new Response(200, file);
            }
            else
            {
                file = new FileInfo(directory.FullName + defaultFileName);
                if (file.Exists)
                {
                    var r = new Response(200, file);
                    await r.Write(stream);
                }
                else
                {
                    var c = new ErrorResponse(404).AsByteArray();
                }
            }
        }

        public void Stop()
        {
            server.Stop();
        }
    }
}
