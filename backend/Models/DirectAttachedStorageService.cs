using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}