using System;


namespace ConsoleApp10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleChat chat = new ConsoleChat();
            ChatNetwork network = new ChatNetwork(chat);

            chat.RunInputLine();
            network.RunServer();

            Console.ReadKey();
        }
    }
}
