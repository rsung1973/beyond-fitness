using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;

using CommonLib.PlugInAdapter;

namespace Utility
{
    /// <summary>
    /// Logger ªººK­n´y­z¡C
    /// </summary>
    public class Logger
    {
        private static ILogger _instance;

        static Logger()
        {
            _instance = CommonLib.Helper.PlugInHelper.GetLogger();
        }

        public static void Shutdown()
        {
            _instance.Shutdown();
        }

        public static void Error(object obj)
        {
            _instance.Error(obj);
        }

        public static void Info(object obj)
        {
            _instance.Info(obj);
        }


        public static void Warn(object obj)
        {
            _instance.Warn(obj);
        }
        public static void Debug(object obj)
        {
            _instance.Debug(obj);
        }

        public static string LogPath
        {
            get
            {
                return _instance.LogPath;
            }
        }
        
        public static string LogDailyPath
        {
            get
            {
                return _instance.LogDailyPath;
            }
        }

        public static void WriteLog()
        {
            _instance.WriteLog();
        }

        public static void SetStream(Stream stream)
        {
            _instance.SetStream(stream);
        }
    }
}
