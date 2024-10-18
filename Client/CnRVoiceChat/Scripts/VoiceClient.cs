
using System.Net.Sockets;
using System.Timers;
using Timer = System.Timers.Timer;

namespace VoiceChat.Client
{
    public class VoiceClient
    {

        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 9999;

        private TcpClient _userClient = null;
        private static NetworkStream _stream = null;
        private static bool _isRunning = false;
        private Timer _connectionHandler = new Timer();

        public event EventHandler<ChatArgs> onVoiceReach;

        public VoiceClient()
        {
            _connectionHandler.Elapsed += new ElapsedEventHandler(CheckConnection);
            _connectionHandler.Interval = 5000;
            _connectionHandler.Enabled = true;
            _connectionHandler.Start();
            Init();
        }


        void Init()
        {
            try
            {
                _userClient = new TcpClient();
                _userClient.Connect(SERVER_IP, SERVER_PORT);
                _stream = _userClient.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Socket Err: " + ex.Message);
            }

            _isRunning = true;
            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
        }

        void CheckConnection(object source, ElapsedEventArgs e)
        {
            if (!_userClient.Connected && _isRunning)
            {
                _userClient.Close();
                string ipAddress = SERVER_IP;
                int port = 9999;
                _userClient = new TcpClient();
                _userClient.Connect(ipAddress, port);
                _stream = _userClient.GetStream();
                Console.WriteLine("Reconnected.");
            }
            else if (!_userClient.Connected && !_isRunning)
            {
                Init();
            }
        }

        public void SendVoice(byte[] buffer, int bytesrecored)
        {
            _stream.Write(buffer, 0, bytesrecored);
        }

        void ReceiveData()
        {
            try
            {
                while (_isRunning)
                {
                    byte[] buffer = new byte[4096 * 5];
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    MemoryStream memoryStream = new(buffer, 0, bytesRead);
                    onVoiceReach?.Invoke(this, new ChatArgs { data = buffer, size = bytesRead });
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
