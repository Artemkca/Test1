using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;


namespace Server
{
    class Server
    {
        List<TcpClient> clients = new List<TcpClient>();
        Dictionary<string, Action<Server, string[]>> handlers = new Dictionary<string, Action<Server, string[]>>();

        public void addRequestHandler(string type, Action<Server, string[]> handler)
        {
            handlers[type] = handler;
        }

        public void sendRequestToAll(string request)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(request);

                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public void clientHandler(TcpClient client)
        {
            byte[] bytes = new byte[4096];

            NetworkStream stream = client.GetStream();

            while (true)
            {
                int length = stream.Read(bytes, 0, bytes.Length);
                string request = Encoding.UTF8.GetString(bytes, 0, length);

                string[] args = request.Split('\n');

                string type = args[0];

                Action<Server, string[]> handler = handlers[type];

                handler(this, args);
            }
        }

        public void runServer()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9090);
            server.Start();

            Console.WriteLine("сервер запущен");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                clients.Add(client);
                new Thread(() => clientHandler(client)).Start();
            }
        }
    }
}
