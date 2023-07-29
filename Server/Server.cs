using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Server
{
    public class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();

        public List<Listener> listeners = new List<Listener>();
        public Queue<Event> events = new Queue<Event>();

        public void CreateEvent(string[] args)
        {
            string type = args[0];

            switch (type)
            {
                case "chatJoin":
                    events.Enqueue(new ChatJoinEvent(this, args[1]));
                    break;
                case "sendMessage":
                    events.Enqueue(new SendMessageEvent(this, args[1], args[2]));
                    break;
            }
        }

        public void RegisterListener(Listener listener)
        {
            listeners.Add(listener);
        }

        public void SendRequestToAll(string request)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(request + '\x4');

                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public void ClientHandler(TcpClient client)
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

                    CreateEvent(args);
                }
            }
            catch {}
            finally
            {
                clients.Remove(client);
            }
        }

        public void RunEvents()
        {
            while (true)
            {
                while (events.Count != 0)
                {
                    Event serverEvent = events.Dequeue();


                    foreach (Listener listener in listeners)
                    {
                        Console.WriteLine(serverEvent.eventName);

                        Handler handler = listener.handlers[serverEvent.eventName];

                        handler(serverEvent);
                    }
                }
            }
        }
        
        public void RunServer()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9090);
            server.Start();

            Console.WriteLine("сервер запущен");

            new Thread(RunEvents).Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                clients.Add(client);
                new Thread(() => ClientHandler(client)).Start();
            }
        }
    }
}
