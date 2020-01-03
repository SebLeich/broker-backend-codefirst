using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class StorageType
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string StorageTypeDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<BlockStorageService> BlockStorageServices { get; set; }
        [JsonIgnore]
        public virtual ICollection<DirectAttachedStorageService> DirectAttachedStorageServices { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public StorageType()
        {
            BlockStorageServices = new HashSet<BlockStorageService>();
            DirectAttachedStorageServices = new HashSet<DirectAttachedStorageService>();
            Projects = new HashSet<Project>();
        }
    }
}