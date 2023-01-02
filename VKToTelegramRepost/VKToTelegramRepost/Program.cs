using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkToTelegram.Logger;

namespace VKToTelegramRepost
{
    class Program
    {
        private const string SETTINGS_FILE = "Settings.json";
        private const string TIMESTAMP_FILE = "TimeStamp.json";
        private static readonly Logger logger = new Logger();
        public static void Main(string[] args)
        {
            try
            {
                logger.Log("Test");

                var settings = new Settings(SETTINGS_FILE)
                .ReadSettings();

                var postReceiver = new VKPostReceiver(GetApi(settings.Token), settings.Group, GetTimeStamp());

                var posts = postReceiver.ReadPosts();

                WriteTimeStamp();
            }
            catch(Exception ex)
            {
                logger.Log(ex.ToString());
            }
        }
        
        private static VkApi GetApi(string token)
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });

            return api;
        }

        private static DateTime GetTimeStamp()
        {
            if (File.Exists(TIMESTAMP_FILE) == false)
            {
                var timeStamp = new TimeStampDto();
                timeStamp.CreateAndWriteFile(TIMESTAMP_FILE);

                return timeStamp.TimeStamp.AddHours(-1);
            }
            var res = JsonConvert.DeserializeObject<TimeStampDto>(File.ReadAllText(TIMESTAMP_FILE))!;

            return res.TimeStamp;
        }

        private static void WriteTimeStamp()
        {
            var timeStamp = new TimeStampDto
            {
                TimeStamp = DateTime.Now
            };

            File.Delete(TIMESTAMP_FILE);

            timeStamp.CreateAndWriteFile(TIMESTAMP_FILE);

            logger.Log("Перезаписана точка отправки");
        }
    }
}