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
        Dictionary<string, Action<string[]>> handlers = new Dictionary<string, Action<string[]>>();

        public void addRequestHandler(string type, Action<string[]> handler)
        {
            handlers[type] = handler;
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

                Action<string[]> handler = handlers[type];

                handler(args);
            }
        }

        public void runServer()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9090);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                new Thread(() => clientHandler(client)).Start();
            }
        }
    }
}
