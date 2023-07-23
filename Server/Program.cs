using System;


namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.addRequestHandler("chat-join", (string[] requestArgs) =>
            {
                Console.WriteLine("requestArgs: " + string.Join(", ", requestArgs));
            });

            server.addRequestHandler("chat-message", (string[] requestArgs) => 
            {
                Console.WriteLine("requestArgs: " + string.Join(", ", requestArgs));    
            });

            server.runServer();

            Console.ReadKey();
        }
    }
}
