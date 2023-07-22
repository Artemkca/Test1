using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp10
{
    class ChatNetwork
    {
        ConsoleChat chat;

        public ChatNetwork(ConsoleChat chat)
        {
            this.chat = chat;
        }

        public void RunServer()
        {
            Console.WriteLine("[INFO] Server started");
        }
    }
}
