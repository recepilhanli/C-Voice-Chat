namespace CnRVoiceChat
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            bool start = false;

            foreach(var arg in args)
            {
                if(arg.Contains("[start]")) start = true;
            }


            ApplicationConfiguration.Initialize();
            Application.Run(new CnRVoiceMain());
        }
    }
}