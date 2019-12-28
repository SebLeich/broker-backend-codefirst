using System;

namespace backend.Models
{
    public class DirectAttachedStorageService :Service
    {
        public bool HasFileEncryption { get; set; }
        public bool HasReplication { get; set; }
        public bool HasFilePermissions { get; set; }
        public bool HasFileLocking { get; set; }
        public bool HasFileCompression { get; set; }
        public Nullable<int> StorageTypeId { get; set; }
        public StorageType StorageType { get; set; }
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.priority > 0)
            {
                Output.priorityHasFileEncryption = Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.priorityHasReplication = Search.hasReplication.priority;
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.priorityHasFilePermissions = Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.priority > 0)
            {
                Output.priorityHasFileLocking = Search.hasFileLocking.priority;
                if (HasFileLocking) Output.pointsHasFileLocking = Search.hasFileLocking.priority;
            }
            if (Search.hasFileCompression != null && Search.hasFileCompression.priority > 0)
            {
                Output.priorityHasFileCompression = Search.hasFileCompression.priority;
                if (HasFileCompression) Output.pointsHasFileCompression = Search.hasFileCompression.priority;
            }
            if (Search.storageType != null && Search.storageType.value != null && Search.storageType.value.Count > 0 && Search.storageType.priority > 0)
            {
                Output.prioritystoragetype = Search.storageType.priority;
                if (StorageTypeId.HasValue && Search.storageType.value.Contains(StorageTypeId.Value)) Output.pointsstoragetype = Search.storageType.priority;
            }
            return Output;
        }
    }
}