using System;


namespace Server
{
    internal class Program
    {
        static void ChatJoinListener(ChatJoinEvent chatJoinEvent) 
        {
            string name = chatJoinEvent.name;
            Server server = chatJoinEvent.server;

            Console.WriteLine(name + " присоеденился");

            server.SendRequestToAll("chat-join\n" + name);
        }
        static void ChatMessageListener(ChatMessageEvent chatMessageEvent)
        {
            string name = chatMessageEvent.name;
            string text = chatMessageEvent.text;

            Server server = chatMessageEvent.server;

            Console.WriteLine(name + ": " + text);

            server.SendRequestToAll("chat-message\n" + name + "\n" + text);
        }

        static void Main(string[] args)
        {
            Server server = new Server();

            server.RegisterListener(ChatJoinListener);
            server.RegisterListener(ChatMessageListener);

            server.RunServer();

            Console.ReadKey();
        }
    }
}
