using System;


namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите свое имя: ");

            string name = Console.ReadLine();


            Console.WriteLine($"Привет {name}");

            Chat chat = new Chat();


            Client client = new Client(name);

            chat.client = client;
            client.chat = chat;


            client.runClient();

            Console.ReadKey();
        }
    }
}
