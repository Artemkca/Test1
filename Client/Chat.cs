using System;


namespace ChatClient
{
    class Chat
    {
        public Client client;

        public static void createInputLine() 
        {
            Console.WriteLine("[INFO] Input line created");
        }

        public static void clientJoin(string name)
        {
            Console.WriteLine("[INFO] Client joined name=" + name);
        }

        public static void printMessage(string name, string text) 
        {
            Console.WriteLine("[INFO] Message name=" + name + " text=" + text);
        }
    }
}
