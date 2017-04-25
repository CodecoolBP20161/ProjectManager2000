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
        public string FileName { get; set; }
        private const string LogPath = "logs/";

        public FileLogger(string fileName)
        {
            FileName = fileName;
            Directory.CreateDirectory("logs");

            if (File.Exists(LogPath + FileName)) return;
            var file = File.CreateText(LogPath + FileName);
            file.Close();
        }

        public void SaveLog(string eventDescripion)
        {
            var file = File.AppendText(LogPath + FileName);
            file.WriteLine(DateTime.Now + " " + eventDescripion);
            file.Close();
        }

    }
}
