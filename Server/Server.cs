using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections;


namespace Server
{
    class Server
    {
        Hashtable handlers = new Hashtable();

        public void addRequestHandler(string type, Action handler)
        {
            handlers.Add(type, handler);
        }

        public void clientHandler(TcpClient client)
        {

        }

        public void runServer()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9090);

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                new Thread(() => clientHandler(client)).Start();
            }
        }
    }
}
