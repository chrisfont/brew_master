
using System;
using System.IO;

namespace bm_logger
{
    /// <summary>
    /// Logger class is a simple singleton to provide a unified interface to our log file + console.
    /// </summary>
    /// <remarks>
    /// This class needs to be upgraded to support console, file or redirected only logging.
    /// </remarks>
    public class Log
    {
        /// <summary>
        /// Logging level for writing log files.
        /// </summary>
        public enum LogLevel
        {
            /// <summary>Error log level. Should be reserved for faults/exceptions</summary>
            Error,
            /// <summary>Warning log level. Should be reserved for recovered errors or aborts.</summary>
            Warning,
            /// <summary>Debug log level. Should be reserved for any normal data needed for debugging issues.</summary>
            Debug,
            /// <summary>Info log level. Should be used for system state output and general messages.</summary>
            Info
        };

        private static Log _instanceLog = null;
        private readonly string _logFile;

        private Log()
        {
            _logFile = Environment.CurrentDirectory + "/" + DateTime.Today.ToString("hh_mm_dd_MM_yyyy") + ".log";
        }

        /// <summary>
        /// Current logging level. Please set this to a lower level to disable some other messages.
        /// </summary>
        /// <remarks>
        /// This needs to be updated to support module specific log levels. For now, it is global.
        /// </remarks>
        public LogLevel Level = LogLevel.Info;

        /// <summary>
        /// Singleton instance accessor.
        /// </summary>
        public static Log Instance => _instanceLog ?? (_instanceLog = new Log());

        /// <summary>
        /// Log a message through this function. TODO: Add log level specific helpers.
        /// </summary>
        /// <param name="logMsg">Message to log. Needs to be a string.</param>
        /// <param name="level">Logging level to use for output.</param>
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
