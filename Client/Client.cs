using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;


namespace ChatClient
{
    class Client
    {
        public string name;
        public TcpClient client;

        public Client(string name)
        {
            Console.WriteLine("[INFO] Client created");

            this.name = name;

            this.client = new TcpClient("25.47.56.202", 9090);
        }

        public void sendRequest(string text)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(text);

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public List<Tuple<string, string>> decodeRequest(string request) 
        {
            List<Tuple<string, string>> args = new List<Tuple<string, string>>();

            foreach(string line in request.Split('\n'))
            {
                string[] keyValue = line.Split('=');

                string key = keyValue[0];
                string value = keyValue[1];

                args.Add(new Tuple<string, string>(key, value));
            }

            return args;
        }

        public void sendJoin()
        {
            sendRequest("type=chat-message\nname=" + name);
        }

        public void sendMessage(string text)
        {
            sendRequest("type=chat-message\nname=" + name + "\ntext=" + text);
        }

        public void runInputRequests()
        {
            byte[] bytes = new byte[4096];

            NetworkStream stream = client.GetStream();

            while (true)
            {
                int length = stream.Read(bytes, 0, bytes.Length);

                string request = Encoding.ASCII.GetString(bytes, 0, length);

                List<Tuple<string, string>> decodedRequest = decodeRequest(request);

                string type = decodedRequest[0].Item2;

                if (type == "chat-message")
                {
                    string name = decodedRequest[1].Item2;
                    string text = decodedRequest[2].Item2;

                    Chat.printMessage(name, text);
                }
                else if (type == "chat-join")
                {
                    string name = decodedRequest[1].Item1;

                    Chat.clientJoin(name);
                }
            }
        }

        public void send()
        {
            Thread.Sleep(1000);

            sendMessage("hello");
        }

        public void runClient()
        {
            new Thread(send).Start();
            new Thread(Chat.createInputLine).Start();
            runInputRequests();
        }
    }
}
