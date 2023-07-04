using NAudio.Wave;
using VoiceChat;

namespace CnRVoiceChat
{


 
    public partial class CnRVoiceMain : Form
    {
       VoiceClient client;

        static string Nickname = "null";




        void OnVoiceReach(object sender, ChatArgs e)
        {
            WaveProvider.Write(e.data, 0, e.size);
        }


        public CnRVoiceMain()
        {
            InitializeComponent();



            var waveIn = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = 0, // indicates which microphone to use
                WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 1),
                BufferMilliseconds = 20
            };
            waveIn.DataAvailable += WaveIn_DataAvailable;

            // WaveOutEvent cihazýný baþlat
            WaveOutEvent waveOut = new WaveOutEvent();
            waveOut.Init(new WaveProvider());

            try
            {
            
            waveIn.StartRecording();

            waveOut.Play();
            }
            catch(Exception ex)
            {
                statelabel.Text = "Hata:" + ex.Message;

            }



            statelabel.Text = "Program baslatildi.";


            client = new VoiceClient();
            client.OnVoiceReach += this.OnVoiceReach;
        }


        void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {


            


            client?.SendVoice(e.Buffer, e.BytesRecorded);

        }


        private void XButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public class WaveProvider : IWaveProvider
        {
            public static BufferedWaveProvider BufferedWaveProvider { get; private set; }

            public WaveFormat WaveFormat => new WaveFormat(44100, 16, 1);

            public int Read(byte[] buffer, int offset, int count)
            {
                return BufferedWaveProvider.Read(buffer, offset, count);
            }

            public static void Write(byte[] buffer, int offset, int count)
            {
                BufferedWaveProvider.AddSamples(buffer, offset, count);
            }

            public WaveProvider()
            {
                BufferedWaveProvider = new BufferedWaveProvider(WaveFormat);
            }
        }

        private void CnRVoiceMain_Load(object sender, EventArgs e)
        {

        }
    }
}