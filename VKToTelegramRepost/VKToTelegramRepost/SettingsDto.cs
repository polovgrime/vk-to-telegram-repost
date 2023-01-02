using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKToTelegramRepost
{
    class SettingsDto : IDto
    {
        public string Token { get; set; } = string.Empty;

        public int Group { get; set; }

        public string ChannelTg { get; set; } = string.Empty;

        public string TokenTg { get; set; } = string.Empty;
    }
}
