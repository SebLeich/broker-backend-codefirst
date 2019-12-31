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
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.Priority > 0)
            {
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.Priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.Priority > 0)
            {
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.Priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.Priority > 0)
            {
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.Priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.Priority > 0)
            {
                if (HasFileLocking) Output.pointsHasFileLocking = Search.hasFileLocking.Priority;
            }
            if (Search.hasFileCompression != null && Search.hasFileCompression.Priority > 0)
            {
                if (HasFileCompression) Output.pointsHasFileCompression = Search.hasFileCompression.Priority;
            }
            if (Search.storageType != null && Search.storageType.Value != null && Search.storageType.Value.Count > 0 && Search.storageType.Priority > 0)
            {
                if (StorageTypeId.HasValue && Search.storageType.Value.Contains(StorageTypeId.Value)) Output.pointsstoragetype = Search.storageType.Priority;
            }
            return Output;
        }
    }
}