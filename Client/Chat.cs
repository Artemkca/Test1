using System;


namespace Client
{
    class Chat
    {
        public static Client client;

        public static void clientJoin(string name)
        {
            Console.WriteLine(name + " зашел");
        }

        public static void printMessage(string name, string text)
        {
            Console.WriteLine(name + ": " + text);
        }

        public static void createInputLine()
        {
            
        }
    }
}
