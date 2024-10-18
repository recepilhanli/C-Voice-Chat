using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using NAudio.Gui;
using NAudio.Wave;

namespace VoiceChat.Client.Configuration
{
    public class Config
    {
        public static Config Instance = new Config();

        private string file = "cnr.ini";
        
        public string Read(string key, string attribute = "VoiceChat")
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(file);
            return data[attribute][key];
        }

        public void Write(string key,string value, string attribute = "VoiceChat")
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(file);
            data[attribute][key] = value;
            parser.WriteFile(file, data);
        }
    }
}
