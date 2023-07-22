using System;


namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Your name: ");
            string name = Console.ReadLine();

            Client client = new Client(name);

            client.runClient();

            Console.ReadKey();
        }
    }
}
