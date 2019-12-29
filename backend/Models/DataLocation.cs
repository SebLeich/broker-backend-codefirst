using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the data location model
    /// </summary>
    public class DataLocation
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DataLocationName { get; set; }
        [ForeignKey("DataLocationType")]
        public Nullable<int> DataLocationTypeId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
        [ForeignKey(nameof(DataLocationTypeId))]
        public virtual DataLocationType DataLocationType { get; set; }
    }
}