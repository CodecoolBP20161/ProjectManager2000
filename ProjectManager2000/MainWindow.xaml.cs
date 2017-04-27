using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectManager2000.Util;

namespace ProjectManager2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileLogger logger = new FileLogger("logs");

        public MainWindow()
        {
            InitializeComponent();
            RenderLogTab();

        }

        private void RenderLogTab()
        {
            logger.SaveLog("Renderin Log Tab", FileLogger.LogType.Debug);

            string[] logFiles = Directory.GetFiles("logs/").Select(System.IO.Path.GetFileName).ToArray();
            LogFilesDropDown.ItemsSource = logFiles;
            LogFilesDropDown.SelectedItem = LogFilesDropDown.Items[0];

            FileLogger fileLogger = new FileLogger(logFiles[0]);
            LogList.ItemsSource = fileLogger.GetLogs();
        }

        private void LogFilesDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedLog = LogFilesDropDown.SelectedItem.ToString();

            logger.SaveLog("Changed to log file: " + selectedLog);

            FileLogger fileLogger = new FileLogger(selectedLog);
            LogList.ItemsSource = fileLogger.GetLogs();
            LogListColumn.Width = LogList.ActualWidth;
        }

        private void LogList_LayoutUpdated(object sender, EventArgs e)
        {
            LogListColumn.Width = LogList.ActualWidth;
        }

        private void RefreshLogsBtn_Click(object sender, RoutedEventArgs e)
        {
            logger.SaveLog("Refreshing logs list");

            FileLogger fileLogger = new FileLogger(LogFilesDropDown.SelectedItem.ToString());
            LogList.ItemsSource = fileLogger.GetLogs();
        }
    }
}
