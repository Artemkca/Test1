using System;


namespace Client
{
    class View
    {
        Client client;

        public View(Client client)
        {
            this.client = client;
        }

        public void clientJoin(string name)
        {
            Console.WriteLine(name + " зашел");
        }

        public void printMessage(string name, string text)
        {
            Console.WriteLine(name + ": " + text);
        }

        public void createInputLine()
        {
            
        }
    }
}
