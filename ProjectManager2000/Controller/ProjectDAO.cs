using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager2000.Model;
using System.IO;
using ProjectManager2000.Util;

namespace ProjectManager2000.Controller
{
    class ProjectDAO
    {
        private const string Path = "Data/";
        private const string FileName = "Projects";
        private readonly FileLogger _logger = new FileLogger("daoLogs");

        public ProjectDAO()
        {
           
            Directory.CreateDirectory("Data");
            var file = File.CreateText(Path + FileName);
            file.Close();
        }

        public void SaveProject(Project project)
        {
            if (Exists(project)) UpdateProject(project);
            else
            {
                var file = File.AppendText(Path + FileName);
                file.WriteLine(project.ToString());
                file.Close();
            }

            _logger.SaveLog($"Project {project.Name} saved");
        }

        public List<Project> getAll()
        {
            List<string> allProjects = File.ReadAllLines(Path + FileName).ToList();
            if (allProjects.Count == 0) return null;
            var projects = new List<Project>();
            allProjects.ForEach((projectString) => projects.Add(Project.fromString(projectString)));

            _logger.SaveLog("All projects are fetched");
            return projects;
        }

        public void UpdateProject(Project project)
        {
            List<String> lines = new List<String>();
            using (StreamReader reader = new StreamReader(File.OpenRead(Path + FileName)))
            {

                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    String[] split = line.Split(',');
                    if (Convert.ToInt64(split[0]) == project.Id)
                    {
                        line = project.ToString();
                    }
                    lines.Add(line);

                }
            }
            using (StreamWriter writer = new StreamWriter(Path + FileName, false))
            {
                {
                    foreach (String newline in lines)
                        writer.WriteLine(newline.Trim());
                }
            }

            _logger.SaveLog($"Project: {project.Name} updated");
        }

        public bool Exists(Project project)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(Path + FileName)))
            {

                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    String[] split = line.Split(',');
                    if (Convert.ToInt64(split[0]) == project.Id)
                    {
                        return true;
                    }

                }


            }
            return false;
        }

        public void DeleteProject(Project project) {
            List<String> lines = new List<String>();
            using (StreamReader reader = new StreamReader(File.OpenRead(Path + FileName)))
            {

                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    String[] split = line.Split(',');
                    if (!(Convert.ToInt64(split[0]) == project.Id)) lines.Add(line);
                }
            }
            using (StreamWriter writer = new StreamWriter(Path + FileName, false))
            {
                    foreach (String newline in lines)
                        writer.WriteLine(newline.Trim());
            }
        }
    }
}
           
