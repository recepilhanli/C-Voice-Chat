using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VoiceChat.Server
{
    class Server
    {
        private const int SERVER_PORT = 9999;

        private static TcpListener _listener = null;
        private static ConcurrentBag<TcpClient> _clients = new ConcurrentBag<TcpClient>();

        static async Task Main(string[] args)
        {
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            _listener = new TcpListener(localAddr, SERVER_PORT);
            _listener.Start();
            Console.WriteLine("Waiting for connections...");

            await HandleClientConnections();
        }

        private static async Task HandleClientConnections()
        {
            try
            {
                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    _clients.Add(client);
                    Console.WriteLine("Client Connection - IP: {0}, Port: {1}", ((IPEndPoint)client.Client.RemoteEndPoint).Address, ((IPEndPoint)client.Client.RemoteEndPoint).Port);

                    _ = Task.Run(() => HandleClientCommunication(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal Error: {0}", ex.Message);
            }
        }

        private static async Task HandleClientCommunication(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0) break; // Client disconnected

                    // Broadcast
                    foreach (TcpClient connectedClient in _clients)
                    {
                        if (connectedClient != client)
                        {
                            NetworkStream connectedStream = connectedClient.GetStream();
                            await connectedStream.WriteAsync(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                _clients.TryTake(out _);
                client.Close();
            }
        }
    }
}