
using System;
using System.IO;

namespace bm_logger
{
    public class Log
    {
        public enum LogLevel
        {
            Error, Warning, Debug, Info
        };

        private static Log _instanceLog = null;
        public LogLevel Level = LogLevel.Info;
        private readonly string _logFile;

        private Log()
        {
            _logFile = Environment.CurrentDirectory + "/" + DateTime.Today.ToString("hh_mm_dd_MM_yyyy") + ".log";
        }

        public static Log Instance => _instanceLog ?? (_instanceLog = new Log());

        public void Message(string logMsg, LogLevel level)
        {
            if (level > Level)
            {
                return;
            }

            var logString = $"{0}:\t{logMsg}\t{level.ToString()}";

            Console.WriteLine(logString);

            using (var file = new StreamWriter(_logFile, true))
            {
                file.WriteLine(logString);
            }
        }
    }
}
