using System;


namespace Server
{
    internal class Program
    {
        static void chatJoin(Server server, string name) 
        {
            Console.WriteLine(name + " присоеденился");

            server.sendRequestToAll("chat-join\n" + name);
        }
        static void chatMessage(Server server, string name, string text)
        {
            Console.WriteLine(name + ": " + text);

            server.sendRequestToAll("chat-message\n" + name + "\n" + text);
        }

        static void Main(string[] args)
        {
            Server server = new Server();

            server.chatJoinListener = chatJoin;
            server.chatMessageListener = chatMessage;

            server.runServer();

            Console.ReadKey();
        }
    }
}
