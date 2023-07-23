using System;


namespace Server
{
    internal class Program
    {
        static void chatJoin(Server server, string[] args) 
        {
            string type = args[0];
            string name = args[1];

            Console.WriteLine(name + " присоеденился");
            server.sendRequestToAll(type + "\n" + name);
        }
        static void chatMessage(Server server, string[] args)
        {
            string type = args[0];
            string name = args[1];
            string text = args[2];

            Console.WriteLine(name + ": " + text);
            server.sendRequestToAll(type + "\n" + name + "\n" + text);
        }

        static void Main(string[] args)
        {
            Server server = new Server();

            server.addRequestHandler("chat-join", chatJoin);
            server.addRequestHandler("chat-message", chatMessage);

            server.runServer();

            Console.ReadKey();
        }
    }
}
