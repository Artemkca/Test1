using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Server
{
    class Server
    {
        List<TcpClient> clients = new List<TcpClient>();

        public Action<Server, string> chatJoinListener;
        public Action<Server, string, string> chatMessageListener;

        public void handle(string[] args)
        {
            string type = args[0];

            string name;
            string text;

            switch (type)
            {
                case "chat-join":
                    name = args[1];

                    chatJoinListener(this, name);
                    break;
                case "chat-message":
                    name = args[1];
                    text = args[2];

                    chatMessageListener(this, name, text);
                    break;
            }
        }

        public void sendRequestToAll(string request)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(request + '\x4');

                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public void clientHandler(TcpClient client)
        {
            byte[] bytes = new byte[4096];

            NetworkStream stream = client.GetStream();

            try
            {
                while (true)
                {
                    int length = stream.Read(bytes, 0, bytes.Length);
                    string request = Encoding.UTF8.GetString(bytes, 0, length);

                    string[] args = request.Split('\n');

                    handle(args);
                }
            }
            catch {}
            finally
            {
                clients.Remove(client);
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
