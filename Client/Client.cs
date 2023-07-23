using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace ChatClient
{
    class Client
    {
        public string name;
        TcpClient client;

        Dictionary<string, Action<Client, string[]>> handlers = new Dictionary<string, Action<Client, string[]>>();


        public Client(string name)
        {
            this.name = name;

            client = new TcpClient("25.47.56.202", 9090);
        }

        public void addRequestHandler(string type, Action<Client, string[]> handler)
        {
            handlers[type] = handler;
        }

        public void sendRequest(string text)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public void runInputRequests()
        {
            byte[] bytes = new byte[4096];

            NetworkStream stream = client.GetStream();

            while (true)
            {
                int length = stream.Read(bytes, 0, bytes.Length);

                string request = Encoding.UTF8.GetString(bytes, 0, length);

                string[] args = request.Split('\n');

                string type = args[0];

                Action<Client, string[]> handler = handlers[type];

                handler(this, args);
            }
        }

        public void runClient()
        {
            new Thread(Chat.createInputLine).Start();
            runInputRequests();
        }
    }
}
