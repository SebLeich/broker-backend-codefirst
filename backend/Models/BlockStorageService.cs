using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BlockStorageService : Service
    {
        public bool HasFileEncryption { get; set; }
        public bool HasReplication { get; set; }

        [ForeignKey("StorageType")]
        public Nullable<int> StorageTypeId { get; set; }

        [ForeignKey(nameof(StorageTypeId))]
        public virtual StorageType StorageType { get; set; }

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
            return Output;
        }
    }
}