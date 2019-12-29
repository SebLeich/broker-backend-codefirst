using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class SearchVector
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int minFulfillmentPercentage { get; set; }
        public SearchVectorListEntry categories { get; set; }
        public SearchVectorListEntry certificates { get; set; }
        public SearchVectorListEntry datalocations { get; set; }
        public SearchVectorListEntry deploymentinfos { get; set; }
        public SearchVectorListEntry models { get; set; }
        public SearchVectorListEntry providers { get; set; }
        public SearchVectorListEntry storageType { get; set; }
        public SearchVectorBooleanEntry hasFileEncryption { get; set; }
        public SearchVectorBooleanEntry hasReplication { get; set; }
        public SearchVectorBooleanEntry hasFilePermissions { get; set; }
        public SearchVectorBooleanEntry hasFileLocking { get; set; }
        public SearchVectorBooleanEntry hasFileCompression { get; set; }
        public SearchVectorBooleanEntry hasDBMS { get; set; }
        public SearchVectorBooleanEntry hasFileVersioning { get; set; }
        public SearchVectorBooleanEntry hasAutomatedSynchronisation { get; set; }

        [ForeignKey("MatchingResponse")]
        public int MatchingResponseId { get; set; }

        [ForeignKey(nameof(MatchingResponseId))]
        public Project MatchingResponse { get; set; }

        public int total { get
            {
                var output = 0;
                if (categories != null && categories.value != null && categories.value.Count > 0) output += categories.priority;
                if (certificates != null && certificates.value != null && certificates.value.Count > 0) output += certificates.priority;
                if (datalocations != null && datalocations.value != null && datalocations.value.Count > 0) output += datalocations.priority;
                if (deploymentinfos != null && deploymentinfos.value != null && deploymentinfos.value.Count > 0) output += deploymentinfos.priority;
                if (models != null && models.value != null && models.value.Count > 0) output += models.priority;
                if (providers != null && providers.value != null && providers.value.Count > 0) output += providers.priority;
                if (storageType != null && storageType.value != null && storageType.value.Count > 0) output += storageType.priority;
                if (hasFileEncryption != null && hasFileEncryption.value.HasValue) output += hasFileEncryption.priority;
                if (hasReplication != null && hasReplication.value.HasValue) output += hasReplication.priority;
                if (hasFilePermissions != null && hasFilePermissions.value.HasValue) output += hasFilePermissions.priority;
                if (hasFileLocking != null && hasFileLocking.value.HasValue) output += hasFileLocking.priority;
                if (hasFileCompression != null && hasFileCompression.value.HasValue) output += hasFileCompression.priority;
                if (hasDBMS != null && hasDBMS.value.HasValue) output += hasDBMS.priority;
                if (hasFileVersioning != null && hasFileVersioning.value.HasValue) output += hasFileVersioning.priority;
                if (hasAutomatedSynchronisation != null && hasAutomatedSynchronisation.value.HasValue) output += hasAutomatedSynchronisation.priority;
                return output;
            }
        }
    }

    public class SearchVectorListEntry
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public List<int> value { get; set; }
        public int priority { get; set; }

        [ForeignKey("SearchVector")]
        public Guid SearchVectorId { get; set; }

        [ForeignKey(nameof(SearchVectorId))]
        public SearchVector SearchVector { get; set; }


        public SearchVectorListEntry()
        {
            value = new List<int>();
        }
    }
    public class SearchVectorBooleanEntry
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Nullable<bool> value { get; set; }
        public int priority { get; set; }

        [ForeignKey("SearchVector")]
        public Guid SearchVectorId { get; set; }

        [ForeignKey(nameof(SearchVectorId))]
        public SearchVector SearchVector { get; set; }
    }
}