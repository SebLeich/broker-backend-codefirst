using Newtonsoft.Json;
using System.Collections.Generic;

namespace backend.Models
{
    public class ProjectServiceType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public ProjectServiceType()
        {
            Projects = new HashSet<Project>();
        }
    }
}