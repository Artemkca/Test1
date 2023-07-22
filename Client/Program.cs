using System;


namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("gandon");

            client.runClient();

            Console.ReadKey();
        }
    }
}
