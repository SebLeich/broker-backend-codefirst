using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class SearchVector
    {
        public int minFulfillmentPercentage { get; set; }
        public SearchVectorEntryList categories { get; set; }
        public SearchVectorEntryList certificates { get; set; }
        public SearchVectorEntryList datalocations { get; set; }
        public SearchVectorEntryList deploymentinfos { get; set; }
        public SearchVectorEntryList models { get; set; }
        public SearchVectorEntryList providers { get; set; }
        public SearchVectorEntryList storageType { get; set; }
        public SearchVectorBooleanEntry hasFileEncryption { get; set; }
        public SearchVectorBooleanEntry hasReplication { get; set; }
        public SearchVectorBooleanEntry hasFilePermissions { get; set; }
        public SearchVectorBooleanEntry hasFileLocking { get; set; }
        public SearchVectorBooleanEntry hasFileCompression { get; set; }
        public SearchVectorBooleanEntry hasDBMS { get; set; }
        public SearchVectorBooleanEntry hasFileVersioning { get; set; }
        public SearchVectorBooleanEntry hasAutomatedSynchronisation { get; set; }

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

    public class SearchVectorEntryList
    {
        public List<int> value { get; set; }
        public int priority { get; set; }

        public SearchVectorEntryList()
        {
            value = new List<int>();
        }
    }
    public class SearchVectorBooleanEntry
    {
        public Nullable<bool> value { get; set; }
        public int priority { get; set; }
    }
}