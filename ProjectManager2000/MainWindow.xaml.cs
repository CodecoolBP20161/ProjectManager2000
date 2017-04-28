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
        Controller.ProjectDAO projectDao = new Controller.ProjectDAO();
        private ProjectDAO projectDAO;
        private Project selectedItem;
        private readonly FileLogger logger = new FileLogger("logs");
        public MainWindow()
        {
            projectDAO = new ProjectDAO();
            this.DataContext = this;
            projectDAO.SaveProject(new Project("Volunti", "Nice thing"));
            projectDAO.SaveProject(new Project("HCCRM", "even nicer thing"));
            projectDAO.SaveProject(new Project("Pisálnomkell.hu", "the whole world is a toilet!"));
            projectDAO.SaveProject(new Project("Test Project4", "whatever"));

            InitializeComponent();
            renderManageTab();
            RenderAssignTab();
            RenderLogTab();
        }
       

        private void RenderLogTab()
        {

            logger.SaveLog("Renderin Log Tab", FileLogger.LogType.Debug);

            string[] logFiles = Directory.GetFiles("logs/").Select(System.IO.Path.GetFileName).ToArray();
            LogFilesDropDown.ItemsSource = logFiles;
            LogFilesDropDown.SelectedItem = LogFilesDropDown.Items[0];

            FileLogger fileLogger = new FileLogger(logFiles[0]);
            LogDataGrid.ItemsSource = fileLogger.GetLogs();
        }

        private void renderManageTab()
        {
            ProjectListMan.ItemsSource = projectDAO.getAll();
        }

        private void LogFilesDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedLog = LogFilesDropDown.SelectedItem.ToString();

            logger.SaveLog("Changed to log file: " + selectedLog);

            FileLogger fileLogger = new FileLogger(selectedLog);
            LogDataGrid.ItemsSource = fileLogger.GetLogs();
        }

        private void RefreshLogsBtn_Click(object sender, RoutedEventArgs e)
        {
            logger.SaveLog("Refreshing logs list");

            FileLogger fileLogger = new FileLogger(LogFilesDropDown.SelectedItem.ToString());
            LogDataGrid.ItemsSource = fileLogger.GetLogs();
        }

        private void projektListAss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            studentList.Items.Clear();
            Model.Project current = (Model.Project) projektListAss.SelectedItem;
            if (current == null) return;
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
            String newParticipiant = applyBox.Text;
            Model.Project current = (Model.Project) projektListAss.SelectedItem;
            if (current == null) return;
            current.AddParticipant(newParticipiant);
            logger.SaveLog(current.Participants.ToString(), FileLogger.LogType.Debug);
            projectDao.UpdateProject(current);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                projectDAO.DeleteProject(selectedItem);
            }
            catch (Exception)
            {

            }
            finally
            {
                ProjectListMan.ItemsSource = projectDAO.getAll();
            }
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
    }
}
