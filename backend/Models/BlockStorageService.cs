using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BlockStorageService : Service
    {
        [ForeignKey("StorageType")]
        public Nullable<int> StorageTypeId { get; set; }

        [ForeignKey(nameof(StorageTypeId))]
        public virtual StorageType StorageType { get; set; }

        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            return Output;
        }
    }
}