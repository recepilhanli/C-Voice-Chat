using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Diagnostics;
using VoiceChat;

namespace CnRVoiceChat
{



    public partial class CnRVoiceMain : Form
    {
        VoiceClient client;

        static string Nickname = "null";


        WaveInEvent waveIn = new NAudio.Wave.WaveInEvent
        {
            DeviceNumber = 0, // indicates which microphone to use
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 1),
            BufferMilliseconds = 20
        };

        WaveOutEvent waveOut = new WaveOutEvent();


        void OnVoiceReach(object sender, ChatArgs e)
        {
            WaveProvider.Write(e.data, 0, e.size);
        }


        public CnRVoiceMain()
        {
            InitializeComponent();

            waveIn.DataAvailable += WaveIn_DataAvailable;
            Init();

        }

        private void Init()
        {



            waveOut.Init(new WaveProvider());

            try
            {

                waveIn.StartRecording();

                waveOut.Play();
            }
            catch (Exception ex)
            {
                statelabel.Text = "Err:" + ex.Message;
                return;
            }



            statelabel.Text = "Program baslatildi.";


            client = new VoiceClient();
            client.OnVoiceReach += OnVoiceReach;
        }


        void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {





            client?.SendVoice(e.Buffer, e.BytesRecorded);

        }


        private void XButton_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }


        public class WaveProvider : IWaveProvider
        {
            public static BufferedWaveProvider bufferedWaveProvider { get; private set; }

            public WaveFormat WaveFormat => new WaveFormat(44100, 16, 1);

            public int Read(byte[] buffer, int offset, int count)
            {
                return bufferedWaveProvider.Read(buffer, offset, count);
            }

            public static void Write(byte[] buffer, int offset, int count)
            {

                bufferedWaveProvider.AddSamples(buffer, offset, count);
            }

            public WaveProvider()
            {
                bufferedWaveProvider = new BufferedWaveProvider(WaveFormat);
            }
        }

        private void CnRVoiceMain_Load(object sender, EventArgs e)
        {

        }

        private void volumeSlider1_Load(object sender, EventArgs e)
        {


        }



        private void Slider1Changed(object sender, EventArgs e)
        {
            try
            {

                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                MMDevice microphone = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

                microphone.AudioEndpointVolume.MasterVolumeLevelScalar = volumeSlider1.Volume;
            }
            catch
            {
                statelabel.Text = "Ses duzeyini ayarlarken bir hata olustu!";
                return;
            }

            statelabel.Text = "Mikrofon ses duzeyi ayarlandi.";

        }

        private void Slider2Changed(object sender, EventArgs e)
        {
            waveOut.Volume = volumeSlider2.Volume;

            statelabel.Text = "Kullanici ses duzeyi ayarlandi.";
        }


    }
}