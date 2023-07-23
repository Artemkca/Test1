using System;
using System.Threading;


namespace Client
{
    class Program
    {
        static Client client;
        static View view;

        static void chatJoin(Client server, string[] args)
        {
            string type = args[0];
            string name = args[1];

            view.clientJoin(name);
        }
        static void chatMessage(Client server, string[] args)
        {
            string type = args[0];
            string name = args[1];
            string text = args[2];

            view.printMessage(name, text);
        }

        static void send(Client client)
        {
            while (true)
            {
                client.sendRequest("chat-message\n" + client.name + "\nтекст сообщения");
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите свое имя: ");

            string name = Console.ReadLine();

            client = new Client(name);
            view = new View(client);

            client.addRequestHandler("chat-join", chatJoin);
            client.addRequestHandler("chat-message", chatMessage);

            client.sendRequest("chat-join\n" + name);
            new Thread(() => send(client)).Start();

            new Thread(() => view.createInputLine()).Start();
            client.runClient();

            Console.ReadKey();
        }
    }
}
