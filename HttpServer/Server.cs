using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HttpServer
{
    public class Server
    {
        
        public const string MSG_DIR = "/root/msg/";
        public const string WEB_DIR = "/root/web/";
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "Http Server Test 1.0";
        private bool Running = false;

        private TcpListener listener;

        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            var serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            Running = true;
            listener.Start();

            while (Running)
            {
                Console.WriteLine("Esperando por conexión...");
                var client = listener.AcceptTcpClient();

                Console.WriteLine("Cliente conectado");
                HandleClient(client);
               
                client.Close();
            }
            Running = false;
            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            var reader = new StreamReader(client.GetStream());
            var msg = string.Empty;

            while(reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
            }
                        
            Debug.WriteLine("Request: \n" + msg);

            var request = Request.GetRequest(msg);
            var response = Response.From(request, msg);
            response.Post(client.GetStream());
        }
    }
}
