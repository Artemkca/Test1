using System;


namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.addRequestHandler();

            server.runServer();

            Console.ReadKey();
        }
    }
}
