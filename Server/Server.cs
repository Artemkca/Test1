using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;


namespace ChatServer
{
    class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();

        public void sendRequest(string text)
        { 
            foreach (TcpClient client in clients) 
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public string[] decodeRequest(string request)
        {
            return request.Split('\n');
        }

        public void handleClient(TcpClient client) 
        {
            try 
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

                        Console.WriteLine("[INFO] Message name=" + name + " text=" + text);
                    }
                    else if (type == "chat-join")
                    {
                        string name = decodedRequest[1];

                        Console.WriteLine("[INFO] Client joined name=" + name);
                    }

                    sendRequest(request);
                }
            }
            catch {}
            finally
            {
                client.Close();
                clients.Remove(client);
            }
        }

        public void runServer()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9090);

            Console.WriteLine("[INFO] Server started");

            server.Start();

            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("[INFO] Client accepted");

                    clients.Add(client);
                    new Thread(() => handleClient(client)).Start();
                }
            }
            catch {}
            finally
            {
                server.Stop();

                Console.WriteLine("[INFO] Server stopped");
            }
        }
    }
}
