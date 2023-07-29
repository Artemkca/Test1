using System;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;


namespace Client
{
    class Network
    {
        public static List<string[]> recv(TcpClient client)
        {

            NetworkStream stream = client.GetStream();

            byte[] bytes = new byte[4096];
            List<string[]> requests = new List<string[]>();

            int length = stream.Read(bytes, 0, bytes.Length);
            string data = Encoding.UTF8.GetString(bytes, 0, length);

            foreach (string request in data.Split('\x4'))
            {
                if (request == "") continue;

                string[] args = request.Split('\n');

                requests.Add(args);
            }

            return requests;
        }

        public static void sendRequest(TcpClient client, string text)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public static void chatJoin(TcpClient client, string name)
        {
            sendRequest(client, "chatJoin\n" + name);
        }

        public static void sendMessage(TcpClient client, string name, string text)
        {
            sendRequest(client, "sendMessage\n" + name + "\n" + text);
        }
    }
}
