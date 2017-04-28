using System;
using ProjectManager2000.Util;

namespace ProjectManager2000.Model
{
    class LogModel
    {
        public DateTime LogTime { get; set; }
        public FileLogger.LogType LogType { get; set; }
        public string LogDescription { get; set; }

        public LogModel(string logDescription, FileLogger.LogType logType)
        {
            this.LogDescription = logDescription;
            this.LogType = logType;
            this.LogTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{LogTime:MM-dd-yy H:mm:ss} -- {LogType} -- {LogDescription}";
        }
    }
}
