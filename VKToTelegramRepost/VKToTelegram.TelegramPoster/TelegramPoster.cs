using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;
using VkToTelegram.Logger;

namespace VKToTelegram.TelegramPoster
{
    public class TelegramPoster
    {
        private string channel;
        private TelegramBotClient client;
        private Logger logger = new Logger();

        public TelegramPoster(string botToken, string channel)
        {
            this.channel = channel.IndexOf("@") != 0 ?  $"@{channel}" : channel;
            this.client = new TelegramBotClient(botToken);
        }

        public async Task<TelegramPoster> Auth()
        {
            await client.GetMeAsync();
            return this;
        }

        public async Task Post(string message, List<string> urls)
        {
            if (string.IsNullOrEmpty(message)) 
            { 
                logger.Log("Post doesn't contain any text");
            }
            else
            {
                await client.SendTextMessageAsync(channel, message);
            }

            if (urls.Count == 0)
            {
                logger.Log("Post doesn't contain any images");
            } 
            else
            {
                await client.SendMediaGroupAsync(channel, urls.Select(e => new InputMediaPhoto(e)));
            }
        }
    }
}