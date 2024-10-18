using CnRVoiceChat;


namespace VoiceChat.Client.Forms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        
        [STAThread]
        static void Main(string[] args)
        {
            //bool start = false;

            //foreach (var arg in args)
            //{
            //    if (arg.Contains("-start")) start = true;
            //}

            //if (!start)
            //{
            //    Process.GetCurrentProcess().Kill();
            //    return;
            //}

            ApplicationConfiguration.Initialize();
            Application.Run(new CnRVoiceMain());
        }
    }
}