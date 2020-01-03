using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// the class contains the cloud service category
    /// </summary>
    public class CloudServiceCategory
    {
        [Key]
        public int Id { get; set; }
        public string CloudServiceCategoryName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public CloudServiceCategory()
        {
            Services = new HashSet<Service>();
            Projects = new HashSet<Project>();
        }
    }
}