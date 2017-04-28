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
        public  Int64 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<String> Participants { get; set; }
        public  DateTime TimeOfCreation { get; set; }

        public Project(String name, String description){
            Id = idCount++;
            TimeOfCreation = DateTime.Now;
            Name = name;
            Description = description;
            Participants = new List<string>();
        }
        public Project(Int64 id, String name, String description,
                       DateTime timeOfCreation, List<String> participants)
        {
            this.Id = id;
            this.TimeOfCreation = timeOfCreation;
            Name = name;
            Description = description;
            Participants = participants;
        }
        public void AddParticipant(String newParticipant) {
            Participants.Add(newParticipant);
        }
        public override string ToString()
        {
            return Id + "," +
                   Name + "," +
                   Description + "," +
                   "{" + string.Join(";", Participants.ToArray()) + "}" + "," +
                   TimeOfCreation;
        }

        public static Project fromString(String String)
        {
            var csvArray = String.Split(',');
            var participantsList = csvArray[3].Trim('{', '}').Split(';').ToList();
            var dateTime = DateTime.Parse(csvArray[4]);
            return new Project(Convert.ToInt64(csvArray[0]), csvArray[1],csvArray[2],dateTime, participantsList);
            
        }
    }
}
