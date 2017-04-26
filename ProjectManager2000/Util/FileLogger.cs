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

        public FileLogger(string fileName)
        {
            FileName = fileName;
            Directory.CreateDirectory("logs");

            if (File.Exists(LogPath + FileName)) return;
            StreamWriter file = File.CreateText(LogPath + FileName);
            file.Close();
        }

        public void SaveLog(string eventDescripion)
        {
            StreamWriter file = File.AppendText(LogPath + FileName);
            file.WriteLine(DateTime.Now + " " + eventDescripion);
            file.Close();
        }

        public List<string> GetLogs()
        {
            var logs = new List<string>();
            StreamReader file = File.OpenText(LogPath + FileName);
            string currentLine;
            while ((currentLine = file.ReadLine()) != null)
            {
                logs.Add(currentLine);
            }
            return logs;
        }
       
    }
}
