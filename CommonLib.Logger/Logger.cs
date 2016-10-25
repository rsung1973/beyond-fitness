using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using CommonLib.Logger.Properties;
using System.Collections.Generic;
using CommonLib.PlugInAdapter;

namespace CommonLib.Logger
{
    /// <summary>
    /// Logger 的摘要描述。
    /// </summary>
    public class Logger : IDisposable , ILogger
    {

        private Queue _errQ;
        private Queue _infoQ;
        private Queue _dbgQ;
        private Queue _warnQ;

        private Dictionary<String,Queue> _hashQ;

        private bool _disposed = false;
        private Thread _thread;
        private string _path;
        private ulong _fileID = 0;
        private Stream _stream;

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
            _errQ = new Queue();
            _infoQ = new Queue();
            _dbgQ = new Queue();
            _warnQ = new Queue();
            _hashQ = new Dictionary<string, Queue>();
            _hashQ.Add("err", _errQ);
            _hashQ.Add("nfo", _infoQ);
            _hashQ.Add("dbg", _dbgQ);
            _hashQ.Add("wrn", _warnQ);

            _thread = new Thread(new ThreadStart(this.run));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Shutdown()
        {
            Thread target = _thread;
            _thread = null;
            if (Thread.CurrentThread != target)
            {
                target.Interrupt();
                target.Join();
            }
        }

        public void Error(object obj)
        {
            _errQ.Enqueue(obj);
            interrupt();
        }

        public void Info(object obj)
        {
            _infoQ.Enqueue(obj);
            interrupt();
        }


        public void Warn(object obj)
        {
            _warnQ.Enqueue(obj);
            interrupt();
        }
        public void Debug(object obj)
        {
            _dbgQ.Enqueue(obj);
            interrupt();
        }

        public string LogPath
        {
            get
            {
                return _path;
            }
        }

        public string LogDailyPath
        {
            get
            {
                string filePath = Path.Combine(_path, String.Format("{0:yyyy}{1}{0:MM}{1}{0:dd}", DateTime.Today, Path.DirectorySeparatorChar));
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                return filePath;
            }
        }

        private void run()
        {

            while (_thread == Thread.CurrentThread)
            {
                try
                {
                    writeLog();

                    Thread.Sleep(Timeout.Infinite);

                }
                catch (ThreadInterruptedException ex)
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void WriteLog()
        {
            writeLog();
        }

        public void SetStream(Stream stream)
        {
            _stream = stream;
        }

        private void writeLog()
        {
            foreach (string qName in _hashQ.Keys)
            {
                Queue _q = (Queue)_hashQ[qName];
                while (_q.Count > 0)
                {
                    object obj = _q.Dequeue();
                    string filePath = LogDailyPath;

                    StringBuilder sb = null;

                    if (obj is ILogObject)
                    {
                        if (obj is ILogObject2)
                        {
                            filePath = ((ILogObject2)obj).GetFileName(filePath, qName, _fileID++);
                            sb = new StringBuilder(obj.ToString());
                        }
                        else
                        {
                            filePath = String.Format("{0}\\{1:000000000000}_({3}).{2}", filePath, _fileID++, qName, ((ILogObject)obj).Subject);
                        }
                    }
                    else
                    {
                        filePath = String.Format("{0}\\SystemLog.{1}", filePath, qName);
                    }

                    using (StreamWriter sw = (_stream == null ? new StreamWriter(filePath, true) : new StreamWriter(_stream)))
                    {
                        if (sb == null)
                        {
                            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            sw.WriteLine(obj.ToString());
                        }
                        else
                        {
                            sw.WriteLine(sb.ToString());
                        }
                        sw.Flush();
                        //                        sw.Close();
                    }
                }
            }
        }

        private void interrupt()
        {
            if (_thread != null)
            {
                _thread.Interrupt();
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
                _thread = null;
                writeLog();
            }
            _disposed = true;
        }

        ~Logger()
        {
            dispose(false);
        }

    }
}
