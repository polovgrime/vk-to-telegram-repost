using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKToTelegramRepost
{
    internal class PostDto
    {
        public string Text { get; set; } = string.Empty;

        public List<Uri> Images { get; set; } = new List<Uri>();

        public string Url { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Text: {Text}, Images: {string.Join(", ", Images)}";
        }
    }
}
