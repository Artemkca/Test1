using System;


namespace Client
{
    class View
    {
        Client client;
        string line;

        public View(Client client)
        {
            this.line = "";
            this.client = client;
        }

        public void clientJoin(string name)
        {
            Console.WriteLine(name + " зашел");
        }
        public void printMessage(string name, string text)
        {
            string printText = name + ": " + text;

            try
            {
                Console.WriteLine("\r" + printText + new string(' ', 96 - printText.Length));
            }
            catch (Exception)
            {
                Console.WriteLine("\r" + printText);
            }
            
            Console.Write("твое сообщение: " + line + new string(' ', 80 - line.Length) + "\r");

        }

        public void createInputLine()
        {
            Console.Write("твое сообщение: ");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                int ord = key.KeyChar - '0';

                if (ord == -35)
                {
                    client.sendMessage(line);
                    line = "";
                }
                else if (ord == -40)
                {
                    try
                    {
                        line = line.Substring(0, line.Length - 1);
                        Console.Write("твое сообщение: " + line + new string(' ', 80 - line.Length) + "\r");
                    }
                    catch (System.ArgumentOutOfRangeException) { }
                }
                else if (line.Length < 80)
                {
                    line += key.KeyChar;
                    Console.Write("твое сообщение: " + line + new string(' ', 80 - line.Length) + "\r");
                }
            }
        }
    }
}
