using CnRVoiceChat;
using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace VoiceChat
{

    public class VoiceClient
    {

        private TcpClient UserClient;
        static NetworkStream stream;
        static bool isRunning;

        public event EventHandler<ChatArgs> OnVoiceReach;


        System.Timers.Timer ConnectionHandler = new System.Timers.Timer();

        private string GetServerIPAddress()
        {
            return "193.164.6.47";
        }


        public VoiceClient()
        {
            ConnectionHandler.Elapsed += new ElapsedEventHandler(this.CheckConnection);
            ConnectionHandler.Interval = 5000;
            ConnectionHandler.Enabled = true;
            ConnectionHandler.Start();

            try
            {
                Init();
            }
            catch
            {

            }

        }




        void Init()
        {

            string ipAddress = GetServerIPAddress();
            int port = 9999;
            UserClient = new TcpClient();
            UserClient.Connect(ipAddress, port);
            stream = UserClient.GetStream();

            isRunning = true;
            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
        }



        void CheckConnection(object source, ElapsedEventArgs e)
        {
            if(!UserClient.Connected && isRunning)
            {
                UserClient.Close();
                string ipAddress = GetServerIPAddress();
                int port = 9999;
                UserClient = new TcpClient();
                UserClient.Connect(ipAddress, port);
                stream = UserClient.GetStream();
                Console.WriteLine("Reconnected.");
            }
            else if (!UserClient.Connected && !isRunning)
            {
                Init();
            }


        }




        public void SendVoice(byte[] buffer, int bytesrecored)
        {
            stream.Write(buffer, 0, bytesrecored);
        }


        void ReceiveData()
        {
            

            try
            {
              
                
                while (isRunning)
                {
                    // Veri alınana kadar bloklanır
                    byte[] buffer = new byte[4096*5]; // Veri tamponu boyutu
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // Ses verisini işleyin ve çalın
                    MemoryStream memoryStream = new MemoryStream(buffer, 0, bytesRead);

                    OnVoiceReach?.Invoke(this,new ChatArgs { data = buffer, size = bytesRead });



                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Socket Err: " + ex.Message);
            }
        }
    }






public class ChatArgs : EventArgs
{
       public byte[] data;
       public int size;
}


}
