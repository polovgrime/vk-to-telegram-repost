using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKToTelegramRepost
{
    internal class TimeStampDto : IDto
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
