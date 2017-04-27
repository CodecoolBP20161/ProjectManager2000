using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager2000.Util
{
    class FileLogger
    {
        private const string LogPath = "logs/";

        public string FileName { get; }
        private StreamWriter fileWriter;

        public FileLogger(string fileName)
        {
            FileName = fileName;
            Directory.CreateDirectory("logs");

            if (File.Exists(LogPath + FileName)) return;
            fileWriter = File.CreateText(LogPath + FileName);
            fileWriter.Close();
        }

        public void SaveLog(string eventDescripion, LogType logType = LogType.Info)
        {
            fileWriter = File.AppendText(LogPath + FileName);
            fileWriter.WriteLine("{0:MM/dd/yy H:mm:ss} -- {1} -- {2}", DateTime.Now, logType, eventDescripion);
            fileWriter.Close();
        }

        public List<string> GetLogs()
        {
            return File.ReadLines(LogPath + FileName).ToList();
        }

        internal enum LogType { Info, Error, Debug}
    }
}
