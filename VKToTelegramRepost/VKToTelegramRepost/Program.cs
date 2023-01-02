using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VKToTelegramRepost
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Test");
            var settings = JsonConvert.DeserializeObject<SettingsDto>(File.ReadAllText("Settings.json"))!;
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = settings.Token
            });

            var res = api.Groups.Get(new GroupsGetParams());

            Console.WriteLine(res.TotalCount);

            Console.ReadLine();
        }
    }
}