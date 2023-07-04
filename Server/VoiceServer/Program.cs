using System;
using System.Net;      //required
using System.Net.Sockets;    //required

namespace Main
{

    class Server
    {

        private static TcpListener Listener;
        private static NetworkStream stream;

        static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {

            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            Listener = new TcpListener(localAddr, 9999);

            Listener.Start();

            Console.WriteLine("Waiting for connections..");

            while (true)
            {

                TcpClient client = Listener.AcceptTcpClient();

                clients.Add(client);

                Console.WriteLine("a client connected. IP: {0}, Port: {1}", ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);


                var clientThread = new System.Threading.Thread(() => HandleClient(client));
                clientThread.Start();
            }

        }



        static void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // broadcast
                    foreach (TcpClient connectedClient in clients)
                    {
                        if (connectedClient != client)
                        {
                            NetworkStream connectedStream = connectedClient.GetStream();
                            connectedStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                clients.Remove(client);

                Console.WriteLine("Error: {0}", ex.Message);

                client.Close();
            }
        }


    }



}

