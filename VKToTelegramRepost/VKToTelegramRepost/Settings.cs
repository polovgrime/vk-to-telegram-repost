using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkToTelegram.Logger;

namespace VKToTelegramRepost
{
    internal class Settings
    {
        private string filename = string.Empty;
        private static Logger logger = new Logger();
        public Settings(string filename)
        { 
            this.filename = filename; 
        }

        private void CheckForSettingsJson()
        {
            if (File.Exists(filename) == false)
            {
                new SettingsDto().CreateAndWriteFile(filename);

                logger.Log($"Pass your access token and group in {filename} file and restart the app");
                Console.ReadLine();

                throw new Exception("No settings file");
            }
        }

        public SettingsDto ReadSettings()
        {
            CheckForSettingsJson();
            return JsonConvert.DeserializeObject<SettingsDto>(File.ReadAllText(filename))!;
        }
    }
}
