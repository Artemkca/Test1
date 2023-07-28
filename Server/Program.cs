using System;
using System.Collections.Generic;


namespace Server
{
    internal class Program
    {
        static void chatJoinListener(ChatJoinEvent chatJoinEvent) 
        {
            string name = chatJoinEvent.name;
            Server server = chatJoinEvent.server;

            Console.WriteLine(name + " присоеденился");

            server.sendRequestToAll("chat-join\n" + name);
        }
        static void chatMessageListener(ChatMessageEvent chatMessageEvent)
        {
            string name = chatMessageEvent.name;
            string text = chatMessageEvent.text;

            Server server = chatMessageEvent.server;

            Console.WriteLine(name + ": " + text);

            server.sendRequestToAll("chat-message\n" + name + "\n" + text);
        }

        static void Main(string[] args)
        {
            Server server = new Server();

            server.registerListener(chatJoinListener);
            server.registerListener(chatMessageListener);

            server.runServer();

            Console.ReadKey();
        }
    }
}
