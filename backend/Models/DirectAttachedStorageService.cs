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
                Output.total += Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.points += Search.hasFileEncryption.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.total += Search.hasReplication.priority;
                if (HasReplication) Output.points += Search.hasReplication.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.total += Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.points += Search.hasFilePermissions.priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.priority > 0)
            {
                Output.total += Search.hasFileLocking.priority;
                if (HasFileLocking) Output.points += Search.hasFileLocking.priority;
            }
            if (Search.hasFileCompression != null && Search.hasFileCompression.priority > 0)
            {
                Output.total += Search.hasFileCompression.priority;
                if (HasFileCompression) Output.points += Search.hasFileCompression.priority;
            }
            if (Search.storageType != null && Search.storageType.value != null && Search.storageType.value.Count > 0 && Search.storageType.priority > 0)
            {
                Output.total += Search.storageType.priority;
                if (StorageTypeId.HasValue && Search.storageType.value.Contains(StorageTypeId.Value)) Output.points += Search.storageType.priority;
            }
            return Output;
        }
    }
}