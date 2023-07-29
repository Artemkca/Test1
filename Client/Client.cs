using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;


namespace Client
{
    class Client
    {
        public string name;
        TcpClient client;

        Dictionary<string, Action<Client, string[]>> handlers = new Dictionary<string, Action<Client, string[]>>();


        public Client(string name)
        {
            this.name = name;

            //client = new TcpClient("25.47.56.202", 9090);
            client = new TcpClient("localhost", 9090);
        }

        public void addRequestHandler(string type, Action<Client, string[]> handler)
        {
            handlers[type] = handler;
        }

        public void sendJoin()
        {
            Network.chatJoin(client, name);
        }

        public void sendMessage(string text)
        {
            Network.sendMessage(client, name, text);
        }

        public void runInputRequests()
        {
            while (true)
            {
                List<string[]> pack = Network.recv(client);

                foreach (string[] args in pack)
                {
                    string type = args[0];

                    Action<Client, string[]> handler = handlers[type];

                    handler(this, args);
                }
            }
        }

        public void runClient()
        {
            runInputRequests();
        }
    }
}
