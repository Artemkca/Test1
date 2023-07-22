using System;
using System.Text;
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
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public string[] decodeRequest(string request) 
        {
            return request.Split('\n');
        }

        public void sendJoin()
        {
            sendRequest("chat-join\n" + name);
        }

        public void sendMessage(string text)
        {
            sendRequest("chat-message\n" + name + "\n" + text);
        }

        public void runInputRequests()
        {
            byte[] bytes = new byte[4096];

            NetworkStream stream = client.GetStream();

            while (true)
            {
                int length = stream.Read(bytes, 0, bytes.Length);

                string request = Encoding.UTF8.GetString(bytes, 0, length);

                string[] decodedRequest = decodeRequest(request);

                string type = decodedRequest[0];

                if (type == "chat-message")
                {
                    string name = decodedRequest[1];
                    string text = decodedRequest[2];

                    Chat.printMessage(name, text);
                }
                else if (type == "chat-join")
                {
                    string name = decodedRequest[1];

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
            sendJoin();

            new Thread(send).Start();
            new Thread(Chat.createInputLine).Start();
            runInputRequests();
        }
    }
}
