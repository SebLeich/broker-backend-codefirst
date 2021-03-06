﻿using Newtonsoft.Json;
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
        public string DataLocationNameDE { get; set; }
        public string DataLocationNameEN { get; set; }
        public string DataLocationNameES { get; set; }

        [ForeignKey("DataLocationType")]
        public Nullable<int> DataLocationTypeId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        [ForeignKey(nameof(DataLocationTypeId))]
        public virtual DataLocationType DataLocationType { get; set; }

        public DataLocation()
        {
            Services = new HashSet<Service>();
            Projects = new HashSet<Project>();
        }
    }
}