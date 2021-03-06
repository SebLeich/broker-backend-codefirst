﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace backend.Models
{
    public class SearchVector
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), ForeignKey(nameof(Project))]
        public int SearchVectorId { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        public Nullable<int> minFulfillmentPercentage { get; set; } = 0;
        public SearchVectorListEntry certificates { get; set; }
        public SearchVectorListEntry datalocations { get; set; }
        public SearchVectorListEntry deploymentinfos { get; set; }
        public SearchVectorListEntry models { get; set; }
        public SearchVectorListEntry providers { get; set; }
        public SearchVectorListEntry storageType { get; set; }
        public SearchVectorListEntry features { get; set; }

        public int total { get
            {
                var output = 0;
                if (certificates != null && certificates.Value != null && certificates.Value.Count > 0) output += certificates.Priority;
                if (datalocations != null && datalocations.Value != null && datalocations.Value.Count > 0) output += datalocations.Priority;
                if (deploymentinfos != null && deploymentinfos.Value != null && deploymentinfos.Value.Count > 0) output += deploymentinfos.Priority;
                if (models != null && models.Value != null && models.Value.Count > 0) output += models.Priority;
                if (providers != null && providers.Value != null && providers.Value.Count > 0) output += providers.Priority;
                if (features != null && features.Value != null && features.Value.Count > 0) output += features.Priority;
                if (storageType != null && storageType.Value != null && storageType.Value.Count > 0) output += storageType.Priority;
                return output;
            }
        }
    }

    public class SearchVectorListEntry
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public List<int> Value { get; set; }
        public int Priority { get; set; }

        [ForeignKey("SearchVector")]
        public int SearchVectorId { get; set; }

        [ForeignKey(nameof(SearchVectorId))]
        public SearchVector SearchVector { get; set; }


        public SearchVectorListEntry()
        {
            Value = new List<int>();
        }
    }
    public class SearchVectorBooleanEntry
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<bool> Value { get; set; }
        public int Priority { get; set; }

        [ForeignKey("SearchVector")]
        public int SearchVectorId { get; set; }

        [ForeignKey(nameof(SearchVectorId))]
        public SearchVector SearchVector { get; set; }
    }
}