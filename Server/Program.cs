using System;


namespace Server
{
    public delegate void d(int a);

    internal class Program
    {
        public static void a(int a) { }

        public static void Main(string[] args)
        {
            Server server = new Server();

            server.RegisterListener(new MainListener(server));

            server.RunServer();

            Console.ReadKey();
        }
    }
}
