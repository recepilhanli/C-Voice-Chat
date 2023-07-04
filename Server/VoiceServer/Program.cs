using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            Console.WriteLine("Sunucu başlatıldı. Bağlantıları dinleniyor...");

            // Bağlantıları dinleme döngüsü
            while (true)
            {
                // Bağlantı isteği geldiğinde kabul et
                TcpClient client = Listener.AcceptTcpClient();

                // Yeni bağlantıyı listeye ekle
                clients.Add(client);

                Console.WriteLine("Yeni istemci bağlandı. IP: {0}, Port: {1}", ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);

                // Yeni istemci için ayrı bir iş parçacığı başlat
                // İstemciden gelen verileri dinlemek ve broadcast yapmak için
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

                    // Bağlı olan tüm istemcilere gelen veriyi broadcast yap
                    foreach (TcpClient connectedClient in clients)
                    {
                        if (connectedClient != client) // Göndereni hariç tut
                        {
                            NetworkStream connectedStream = connectedClient.GetStream();
                            connectedStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda buraya ulaşılır

                // İstemciyi listeden kaldır
                clients.Remove(client);

                Console.WriteLine("Hata: {0}", ex.Message);

                // İstemciyi kapat
                client.Close();
            }
        }


    }



}

