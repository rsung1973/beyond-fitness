using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Collections.Generic;
using CommonLib.Properties;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// Logger 的摘要描述。
    /// </summary>
    public class Logger : ILoggerProvider, ILogger
    {
        private static readonly Logger _instance = new Logger();

        private Dictionary<LogLevel, Queue<String>> _hashQ;
        private Dictionary<LogLevel, Queue<String>> _standbyQ;

        public readonly LogLevel[] LoggingLevel = new LogLevel[]
        {
            LogLevel.Error,
            LogLevel.Information,
            LogLevel.Debug,
            LogLevel.Warning,
        };

        private bool _disposed = false;
        private string _path;
        private Stream _stream;
        private int _waiting = 10;

        public Logger()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
            if (!String.IsNullOrEmpty(Settings.Default.LogPath))
                _path = Settings.Default.LogPath;
            else
                _path = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), "logs");

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            _hashQ = new Dictionary<LogLevel, Queue<String>>();
            foreach (var qName in LoggingLevel)
            {
                _hashQ.Add(qName, new Queue<String>());
            }

            _standbyQ = new Dictionary<LogLevel, Queue<String>>();
            foreach (var qName in LoggingLevel)
            {
                _standbyQ.Add(qName, new Queue<String>());
            }

            run();

        }

        public void Shutdown()
        {

        }

        public static void Error(String obj)
        {
            _instance._hashQ[LogLevel.Error].Enqueue(obj);
        }

        public static void Info(String obj)
        {
            _instance._hashQ[LogLevel.Information].Enqueue(obj);
        }


        public static void Warn(String obj)
        {
            _instance._hashQ[LogLevel.Warning].Enqueue(obj);
        }

        public static void Debug(String obj)
        {
            _instance._hashQ[LogLevel.Debug].Enqueue(obj);
        }

        public static string LogPath
        {
            get
            {
                return _instance._path;
            }
        }

        public static string LogDailyPath
        {
            get
            {
                string filePath = Path.Combine(_instance._path, String.Format("{0:yyyy}{1}{0:MM}{1}{0:dd}", DateTime.Today, Path.DirectorySeparatorChar));
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                return filePath;
            }
        }

        private void run()
        {
            var t = Task.Run(() =>
            {
                writeLog();
            });

            t = t.ContinueWith(ts =>
            {
                Task.Delay(_waiting).ContinueWith(ts1 =>
                {
                    run();
                });
            });
        }
        public void WriteLog()
        {
            writeLog();
        }

        public void SetStream(Stream stream)
        {
            _stream = stream;
        }

        public static TextWriter OutputWritter { get; set; }

        private void writeLog()
        {
            bool hasContent = false;
            foreach (var qName in LoggingLevel)
            {
                Queue<String> workingQ = _hashQ[qName];

                if (workingQ.Count == 0)
                {
                    continue;
                }

                hasContent = true;

                _hashQ[qName] = _standbyQ[qName];
                _standbyQ[qName] = workingQ;

                while (workingQ.Count > 0)
                {
                    String obj = workingQ.Dequeue();
                    if (obj == null)
                        continue;

                    string filePath = String.Format("{0}\\SystemLog.{1}", LogDailyPath, qName);

                    if (OutputWritter != null)
                    {
                        OutputWritter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        OutputWritter.WriteLine(obj);
                    }

                    using (StreamWriter sw = (_stream == null ? new StreamWriter(filePath, true) : new StreamWriter(_stream)))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        sw.WriteLine(obj.ToString());
                        sw.Flush();
                        //                        sw.Close();
                    }
                }
            }

            if (hasContent)
            {
                _waiting = 10;
            }
            else
            {
                _waiting = 5000;
            }
        }


        #region IDisposable 成員

        public void Dispose()
        {
            // TODO:  加入 Logger.Dispose 實作
            dispose(true);
        }

        #endregion

        protected void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("Object is disposing now ...");
                }
                else
                {
                    Console.WriteLine("May destructor run ...");
                }

                writeLog();
            }
            _disposed = true;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return this;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Error:
                    Error(formatter(state, exception));
                    break;

                case LogLevel.Debug:
                    Debug(formatter(state, exception));
                    break;

                case LogLevel.Warning:
                    Warn(formatter(state, exception));
                    break;

                default:
                    Info(formatter(state, exception));
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        ~Logger()
        {
            dispose(false);
        }

    }
}
