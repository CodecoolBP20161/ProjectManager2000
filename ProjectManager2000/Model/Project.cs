using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager2000.Util;

namespace ProjectManager2000.Model
{
    class Project {
        private static int idCount = 1;
        public readonly Int64 id;
        public String Name { get; set; }
        public String Description { get; set; }
        public List<String> Participants { get; set; }
        private readonly DateTime timeOfCreation;

        public Project(String name, String description){
            id = idCount++;
            timeOfCreation = DateTime.Now;
            Name = name;
            Description = description;
            Participants = new List<string>();
        }
        public Project(Int64 id, String name, String description,
                       DateTime timeOfCreation, List<String> participants)
        {
            this.id = id;
            this.timeOfCreation = timeOfCreation;
            Name = name;
            Description = description;
            Participants = participants;
        }
        public void AddParticipant(String newParticipant) {
            Participants.Add(newParticipant);
        }
        public override string ToString()
        {
            return id + "," +
                   Name + "," +
                   Description + "," +
                   "{" + string.Join(";", Participants.ToArray()) + "}" + "," +
                   timeOfCreation;
        }

        public static Project fromString(String String)
        {
            var csvArray = String.Split(',');
            var participantsList = csvArray[3].Trim('{', '}').Split(';').ToList();
            var dateTime = Convert.ToDateTime(csvArray[4]);
            return new Project(Convert.ToInt64(csvArray[0]), csvArray[1],csvArray[5],dateTime, participantsList);
            
        }
    }
}
