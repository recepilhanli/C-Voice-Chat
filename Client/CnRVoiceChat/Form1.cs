
using NAudio.CoreAudioApi;
using NAudio.Gui;
using NAudio.Wave;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Timers;
using Client;
using Vector;
using Configuration;

namespace CnRVoiceChat
{



    public partial class CnRVoiceMain : Form
    {
        private VoiceClient client;

        MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        MMDevice microphone;

        char pushToTalkKey = 'K';
        bool pushToTalk = false;
        bool isTalking = false;
        //[DllImport("STCnR.asi")]

        //private static extern FVector GetPlayerPos(); 



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



            if (!pushToTalk) waveIn.DataAvailable += WaveIn_DataAvailable;

            waveOut.Init(new WaveProvider());

            Init();
            //Vector location = Vector.Serialize(GetPlayerPos());
            //MessageBox.Show(location.x + " - " + location.y + " - " + location.z);
        }

        private void Init()
        {

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            microphone = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

            GetConfig();
            try
            {


                waveIn.StartRecording();

                waveOut.Play();
            }
            catch (Exception ex)
            {
                statelabel.Text = "Mikrofon bulunamadi, tekrar deneniyor..";

                return;
            }

            statelabel.Text = "Program baslatildi.";
            retrybutton.Click -= retrybutton_Click;
            retrybutton.Visible = false;

            client = new VoiceClient();
            client.OnVoiceReach += OnVoiceReach;

        }



        void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {


            //float maxValue = 0;
            //for (int i = 0; i < e.BytesRecorded; i += 2)
            //{
            //    short sample = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i]);
            //    float sampleValue = sample / 32768f;
            //    maxValue = Math.Max(maxValue, Math.Abs(sampleValue));
            //}


            //float threshold = 0.25f;

            //if (maxValue >= threshold)
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



        private void Slider1Changed(object sender, EventArgs e)
        {
            try
            {
                microphone.AudioEndpointVolume.MasterVolumeLevelScalar = volumeSlider1.Volume;
            }
            catch
            {
                statelabel.Text = "Ses duzeyini ayarlarken bir hata olustu!";
                return;
            }

            statelabel.Text = "Mikrofon ses seviyesi ayarlandi.";

            Config.Instance.Write("Mikrofon Ses Seviyesi", volumeSlider1.Volume.ToString());

        }

        private void Slider2Changed(object sender, EventArgs e)
        {
            waveOut.Volume = volumeSlider2.Volume;

            statelabel.Text = "Kullanici ses seviyesi ayarlandi.";

            Config.Instance.Write("Kullanici Ses Seviyesi", volumeSlider1.Volume.ToString());
        }

        private void retrybutton_Click(object sender, EventArgs e)
        {
            Init();
        }


        private void Check(object sender, EventArgs e)
        {
            //var processes = Process.GetProcessesByName("gta_sa.exe");
            //if (processes.Length == 0) Process.GetCurrentProcess().Kill();


        }

        private void CnRVoiceMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }



        public void GetConfig()
        {
            string path = "cnr.ini";
            if (!File.Exists(path))
            {
                CreateConfigFile();
                return;
            }


            checkBox1.Checked = Convert.ToBoolean(Config.Instance.Read("Bas-Konus"));
            pushToTalk = checkBox1.Checked;


            pushToTalkKey = char.Parse(Config.Instance.Read("Bas-Konus Tusu"));


            float volume = float.Parse(Config.Instance.Read("Mikrofon Ses Seviyesi"));
            volumeSlider1.Volume = volume;
            microphone.AudioEndpointVolume.MasterVolumeLevelScalar = volumeSlider1.Volume;


            volume = float.Parse(Config.Instance.Read("Kullanici Ses Seviyesi"));
            volumeSlider2.Volume = volume;
            waveOut.Volume = volumeSlider2.Volume;


        }



        void CreateConfigFile()
        {
            string path = "cnr.ini";

            try
            {

                File.Create(path).Close();
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("; SA:MP Turkiye CnR Client Ayarlari");
                writer.WriteLine("; Ayarlarda false -> Kapali, true -> Acik anlamina gelir.");
                writer.WriteLine("; Ses Seviyesi ayarlari 0-1 arasi ondalik sayilarla olmalidir.");
                writer.WriteLine("[Sesli Sohbet]");
                writer.WriteLine("Bas-Konus = false");
                writer.WriteLine("Yakinlik Hassasiyeti = false");
                writer.WriteLine("Bas-Konus Tusu = K");
                writer.WriteLine("Mikrofon Ses Seviyesi = 1.0");
                writer.WriteLine("Kullanici Ses Seviyesi = 1.0");
                writer.Close();
            }
            catch
            {
                MessageBox.Show("Ayarlr dosyasi olusturulamadi!", "SA:MP Turkiye CnR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void CnRVoiceMain_KeyDown(object sender, KeyEventArgs e)
        {
            char key = Char.ToLower((char)e.KeyCode);
            char userKey = Char.ToLower(pushToTalkKey);

            if (key == userKey && pushToTalk && !isTalking)
            {
                isTalking = true;
                waveIn.DataAvailable += WaveIn_DataAvailable;
            }

        }

        private void CnRVoiceMain_KeyUp(object sender, KeyEventArgs e)
        {
            char key = Char.ToLower((char)e.KeyCode);
            char userKey = Char.ToLower(pushToTalkKey);

            if (key == userKey && pushToTalk && isTalking)
            {
                isTalking = false;
                waveIn.DataAvailable -= WaveIn_DataAvailable;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //push to talk
        {
            pushToTalk = checkBox1.Checked;

           

            if (pushToTalk)
            {
                Config.Instance.Write("Bas-Konus", "true");
                statelabel.Text = ($"{pushToTalkKey} tusu ile konusabilirsiniz.");
                waveIn.DataAvailable -= WaveIn_DataAvailable;
            }
            else
            {
                Config.Instance.Write("Bas-Konus", "false");
                waveIn.DataAvailable += WaveIn_DataAvailable;
            }
        }
    }
}