using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public delegate void Handler(Event method);

    public class Listener
    {
        public Server server;
        public Dictionary<string, Handler> handlers = new Dictionary<string, Handler>();

        public Listener(Server server)
        {
            this.server = server;
        }

        public void RegisterHandler(string eventName, Handler listener)
        {
            handlers[eventName] = listener;
        }
    }

    public class MainListener : Listener
    {
        public MainListener(Server server) : base(server) 
        {
            RegisterHandler("chatJoin", _event => ChatJoinHandler((ChatJoinEvent) _event));
            RegisterHandler("sendMessage", _event => SendMessageHandler((SendMessageEvent) _event));
        }

        public void ChatJoinHandler(ChatJoinEvent chatJoinEvent)
        {
            string name = chatJoinEvent.name;

            Server server = chatJoinEvent.server;

            Console.WriteLine(name + " присоеденился");

            server.SendRequestToAll("chatJoin\n" + name);
        }

        public void SendMessageHandler(SendMessageEvent chatMessageEvent)
        {
            string name = chatMessageEvent.name;
            string text = chatMessageEvent.text;

            Server server = chatMessageEvent.server;

            Console.WriteLine(name + ": " + text);

            server.SendRequestToAll("sendMessage\n" + name + "\n" + text);
        }
    }
}
