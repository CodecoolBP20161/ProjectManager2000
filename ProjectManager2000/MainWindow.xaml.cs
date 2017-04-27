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
using ProjectManager2000.Controller;
using ProjectManager2000.Model;

namespace ProjectManager2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectDAO projectDAO;
        private Project selectedItem;
        public MainWindow()
        {
            projectDAO = new ProjectDAO();
            InitializeComponent();
            this.DataContext = this;
            //RenderLogTab();
            renderManageTab();
        }

        private void RenderLogTab()
        {
            string[] logFiles = Directory.GetFiles("logs/").Select(System.IO.Path.GetFileName).ToArray();
            LogFilesDropDown.ItemsSource = logFiles;
            LogFilesDropDown.SelectedItem = LogFilesDropDown.Items[0];

            FileLogger fileLogger = new FileLogger(logFiles[0]);
            LogList.ItemsSource = fileLogger.GetLogs();
        }
        private void renderManageTab()
        {
            projectDAO.SaveProject(new Project("Volunti", "Nice thing"));
            projectDAO.SaveProject(new Project("HCCRM", "even nicer thing"));
            projectDAO.SaveProject(new Project("Pisálnomkell.hu", "the whole world is a toilet!"));
            projectDAO.SaveProject(new Project("Test Project4", "whatever"));
            ProjectListMan.ItemsSource = projectDAO.getAll();


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
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            projectDAO.DeleteProject(selectedItem);
            ProjectListMan.ItemsSource = projectDAO.getAll();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            String projectName = projectNameInput.Text;
            String projectDesc = new TextRange(projectDescInput.Document.ContentStart, projectDescInput.Document.ContentEnd).Text.Replace(Environment.NewLine, " ");
            projectDAO.SaveProject(new Project(projectName, projectDesc));
            ProjectListMan.ItemsSource = projectDAO.getAll();
        }

        private void ProjectListMan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (Project) ProjectListMan.SelectedItem;
        }

        private void LogList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
