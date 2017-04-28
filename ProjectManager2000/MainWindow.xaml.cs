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
        Controller.ProjectDAO projectDao = new Controller.ProjectDAO();
        public MainWindow()
        {
            InitializeComponent();
            RenderAssignTab();
            //RenderLogTab();
        }

        private void RenderLogTab()
        {

            string[] logFiles = Directory.GetFiles("logs/").Select(System.IO.Path.GetFileName).ToArray();
            LogFilesDropDown.ItemsSource = logFiles;
            LogFilesDropDown.SelectedItem = LogFilesDropDown.Items[0];

            FileLogger fileLogger = new FileLogger(logFiles[0]);
            LogList.ItemsSource = fileLogger.GetLogs();
        }



        private void LogFilesDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileLogger fileLogger = new FileLogger(LogFilesDropDown.SelectedItem.ToString());
            LogList.ItemsSource = fileLogger.GetLogs();
            LogListColumn.Width = LogList.ActualWidth;
        }

        private void LogList_LayoutUpdated(object sender, EventArgs e)
        {
            LogListColumn.Width = LogList.ActualWidth;
        }

        private void projektListAss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            studentList.Items.Clear();
            Model.Project current = (Model.Project) projektListAss.SelectedItem;
            foreach (String participiant in current.Participants)
            {
                studentList.Items.Add(participiant);
            }
            descField.Items.Clear();
            descField.Items.Add(current.Description);
        }
        private void RenderAssignTab()
        {
            List<Model.Project> projectList = projectDao.getAll();
            foreach (Model.Project proj in projectList)
            {
                projektListAss.Items.Add(proj);
            }
        }

        private void refreshPList_Click(object sender, RoutedEventArgs e)
        {
            projektListAss.Items.Clear();
            List<Model.Project> projectList = projectDao.getAll();
            foreach (Model.Project proj in projectList)
            {
                projektListAss.Items.Add(proj);
            }
        }

        private void studentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void applyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            String newParticipiant =  applyBox.Text;
            Model.Project current = (Model.Project)projektListAss.SelectedItem;
            current.Participants.Add(newParticipiant);
            projectDao.UpdateProject(current);
        }
    }
}
