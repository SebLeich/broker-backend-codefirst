using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Feature
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DescriptionDE { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionES { get; set; }
        public string InternalDescription { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        [JsonIgnore]
        public virtual List<Service> Services { get; set; }

        public Feature()
        {
            Services = new List<Service>();
        }
    }
}