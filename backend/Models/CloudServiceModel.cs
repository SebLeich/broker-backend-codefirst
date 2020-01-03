using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// the class contains the cloud servive model
    /// </summary>
    public class CloudServiceModel
    {
        [Key]
        public int Id { get; set; }
        public string CloudServiceModelName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public CloudServiceModel()
        {
            Services = new HashSet<Service>();
            Projects = new HashSet<Project>();
        }
    }
}