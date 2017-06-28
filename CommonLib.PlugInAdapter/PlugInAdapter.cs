using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLib.PlugInAdapter
{
    public interface IPdfUtility
    {
        void ConvertHtmlToPDF(String htmlFile, String pdfFile, double timeOutInMinute);
    }

    public interface ILogger
    {
        void Debug(object obj);
        void Info(object obj);
        void Error(object obj);
        void Warn(object obj);
        void SetStream(Stream stream);
        void Shutdown();
        void WriteLog();

        string LogDailyPath { get; }
        string LogPath { get; }
    }

    public interface ILogObject
    {
        string Subject
        {
            get;
        }

        string ToString();

    }

    public interface ILogObject2 : ILogObject
    {
        string GetFileName(string currentLogPath, string qName, ulong key);
    }

}
