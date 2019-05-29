using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Http
{
    public class ServerHTTP
    {
        private TcpListener server = new TcpListener(IPAddress.Any, 80);
        private TcpClient client;
        private string path;
        private NetworkStream stream;

        public ServerHTTP(string path)
        {
            this.path = path;
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

        private void AcceptClient()
        {
            while (true)
            {
               client = server.AcceptTcpClient();
               ReadFromClient(client);
            }
        }

        private void ReadFromClient(TcpClient client)
        {
            stream = client.GetStream();
            byte[] message = new byte[1024];
            int bytesRead = stream.Read(message, 0, message.Length);
            Array.Resize(ref message,bytesRead);
            string ex = Encoding.ASCII.GetString(message);
            Console.WriteLine(ex);

            string responseBody = File.ReadAllText(@"c:\users\andreea\test.html");
            string responseHeader = $"HTTP/1.1 200 OK\r\nContent-Length: {responseBody.Length}\r\n\r\n";
            stream.Write(Encoding.UTF8.GetBytes(responseHeader));
            stream.Write(Encoding.UTF8.GetBytes(responseBody));
            stream.Close();
        }

        private void Execute(DirectoryInfo folder)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            server.Stop();
        }
    }
}
