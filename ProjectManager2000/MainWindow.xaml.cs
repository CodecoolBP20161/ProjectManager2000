using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectManager2000.Util;

namespace ProjectManager2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileLogger _fileLogger = new FileLogger("testLog");
        private List<string> _logList;

        public MainWindow()
        {
            InitializeComponent();
            _fileLogger.SaveLog("app started!");
            LogChooser.Minimum = 0;
            LogChooser.Maximum = _fileLogger.GetLogs().Count - 1;
            _logList = _fileLogger.GetLogs();
            TestLabel.Content = _logList[0];
        }

        private void LogChooser_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TestLabel.Content = _logList[(int) LogChooser.Value];
        }
    }
}
