using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkToTelegram.Logger
{
    public class Logger
    {
        private const string LOGFILE_NAME = "log-{0}.log";
        private string path = ".\\logs\\";
        private string filename;
        private static readonly object syncObj = new object();
        public Logger()
        {
            filename = path + string.Format(LOGFILE_NAME, DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));
            CreateFile();
        }

        public Logger(string path)
        {
            this.path = path;
            filename = path + string.Format(LOGFILE_NAME, DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));
            CreateFile();
        }

        private void CreateFile()
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists(filename) == false)
            {
                var file = File.Create(filename);
                file.Dispose();
            }
        }

        public void Log(string message)
        {
            message += $" |{DateTime.Now}";
            Console.WriteLine(message);
            WriteToFile(message);
        }

        private void WriteToFile(string message)
        {
            lock (syncObj)
            {
                using (var sw = File.AppendText(filename))
                {
                    sw.WriteLine(message);
                }
            }
        }

        public void Error(string message) 
        {
            Log("Error! " + message);
        }
    }
}
