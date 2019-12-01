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

    }
}