using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the provider model
    /// </summary>
    public class Provider
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string URL { get; set; }
        public string Revision { get; set; }
        public bool Verified { get; set; } = false;

        public virtual ICollection<ProviderPayment> ProviderPayments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public Provider()
        {
            Services = new HashSet<Service>();
            Projects = new HashSet<Project>();
        }
    }
}