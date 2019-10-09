using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class BlockStorageService : Service
    {
        public bool HasFileEncryption { get; set; }
        public bool HasReplication { get; set; }

        public Nullable<int> StorageTypeId { get; set; }
        public StorageType StorageType { get; set; }

    }
}