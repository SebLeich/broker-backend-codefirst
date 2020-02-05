using System;

namespace backend.Models
{
    public class DirectAttachedStorageService :Service
    {
        public Nullable<int> StorageTypeId { get; set; }
        public StorageType StorageType { get; set; }
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.storageType != null && Search.storageType.Value != null && Search.storageType.Value.Count > 0 && Search.storageType.Priority > 0)
            {
                if (StorageTypeId.HasValue && Search.storageType.Value.Contains(StorageTypeId.Value)) Output.pointsstoragetype = Search.storageType.Priority;
            }
            return Output;
        }
    }
}