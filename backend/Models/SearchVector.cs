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
                if (categories != null) output += categories.priority;
                if (certificates != null) output += certificates.priority;
                if (datalocations != null) output += datalocations.priority;
                if (deploymentinfos != null) output += deploymentinfos.priority;
                if (models != null) output += models.priority;
                if (providers != null) output += providers.priority;
                if (storageType != null) output += storageType.priority;
                if (hasFileEncryption != null) output += hasFileEncryption.priority;
                if (hasReplication != null) output += hasReplication.priority;
                if (hasFilePermissions != null) output += hasFilePermissions.priority;
                if (hasFileLocking != null) output += hasFileLocking.priority;
                if (hasFileCompression != null) output += hasFileCompression.priority;
                if (hasDBMS != null) output += hasDBMS.priority;
                if (hasFileVersioning != null) output += hasFileVersioning.priority;
                if (hasAutomatedSynchronisation != null) output += hasAutomatedSynchronisation.priority;
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
        public bool value { get; set; }
        public int priority { get; set; }
    }
}