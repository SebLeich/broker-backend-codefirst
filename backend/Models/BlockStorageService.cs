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
            return Output;
        }
    }
}