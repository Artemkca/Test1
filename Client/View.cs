using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

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
            Console.WriteLine("\r" + name + ":" + text + new string(' ', 80 - line.Length));
            Console.Write(line + new string(' ', 80 - line.Length) + "\r");

        }

        public void createInputLine()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

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
                        Console.Write(line + new string(' ', 80 - line.Length) + "\r");
                    }
                    catch (System.ArgumentOutOfRangeException) { }

                    line = line.Substring(0, line.Length - 1);

                    Console.Write(line + new string(' ', 80 - line.Length) + "\r");
                }
                else
                {
                    line += key.KeyChar;
                    Console.Write(line + new string(' ', 80 - line.Length) + "\r");

                }


            }
        }
    }
}
