using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkToTelegram.Logger;
using VKToTelegram.TelegramPoster;

namespace VKToTelegramRepost
{
    class Program
    {
        private const string SETTINGS_FILE = "Settings.json";
        private const string TIMESTAMP_FILE = "TimeStamp.json";
        private static readonly Logger logger = new Logger();
        private const int LIMIT_FOR_SENDING = 5;
        public static async Task Main(string[] args)
        {
            try
            {
                logger.Log("Начало работы приложения");

                var settings = new Settings(SETTINGS_FILE)
                    .ReadSettings();

                var postReceiver = new VKPostReceiver(GetApi(settings.Token), settings.Group, GetTimeStamp());

                var posts = postReceiver.ReadPosts();

                var poster = await (new TelegramPoster(settings.TokenTg, settings.ChannelTg)).Auth();
                
                var currentIdx = 1;

                foreach(var post in posts)
                {
                    try
                    {
                        logger.Log($"Post {currentIdx++} out of {posts.Count}");
                        logger.Log($"Url: {post.Url}");
                        await poster.Post(post.Text, post.Images.Select(e => e.ToString()).ToList());
                                                
                        if (post.Images.Count > 1)
                        {
                            logger.Log("Post contains multiple images. Awaiting 60 seconds");
                            Thread.Sleep(60 * 1000);
                        } 
                        else if (currentIdx % LIMIT_FOR_SENDING == 0)
                        {
                            logger.Log("Awaiting 60 seconds for telegram api. Reached limit for posting.");
                            Thread.Sleep(60 * 1000);
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString() + "\n post:" + JsonConvert.SerializeObject(post));
                    }
                }

                WriteTimeStamp();

                logger.Log("Done");
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
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