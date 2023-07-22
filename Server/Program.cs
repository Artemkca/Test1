using System;


namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.runServer();

            Console.ReadKey();
        }
    }
}
