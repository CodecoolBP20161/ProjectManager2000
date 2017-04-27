using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager2000.Model;

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
            LogModel logModel = new LogModel(eventDescripion, logType);
            fileWriter = File.AppendText(LogPath + FileName);
            fileWriter.WriteLine(logModel);
            fileWriter.Close();
        }

        public List<LogModel> GetLogs()
        {
            List<LogModel> logModels = new List<LogModel>();
            List<string> listOfLogStrings = File.ReadLines(LogPath + FileName).ToList();
            foreach (string logString in listOfLogStrings)
            {
                if (logString.Equals("")) return logModels;
                var logDatasList = logString.Split(new[]{" -- "}, StringSplitOptions.None);
                LogModel logModel = new LogModel(logDatasList[2], ParseLogType(logDatasList[1]))
                {
                    LogTime = DateTime.ParseExact(logDatasList[0], "MM-dd-yy H:mm:ss", CultureInfo.InvariantCulture)
                };
                logModels.Add(logModel);
            }
            return logModels;
        }

        internal enum LogType { Info, Error, Debug }

        private LogType ParseLogType(string type)
        {
            switch (type)
            {
                case "Error":
                    return LogType.Error;
                case "Debug":
                    return LogType.Debug;
            }
            return LogType.Info;
        }
    }
}
