using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKToTelegramRepost
{
    static class FileHelperEx
    {
        public static void CreateAndWriteFile<T>(this T data, string path)
            where T : IDto
        {
            var file = File.Create(path);
            file.Dispose();

            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }
    }
}
