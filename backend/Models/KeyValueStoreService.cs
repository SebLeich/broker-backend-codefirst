using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains a key value store service
    /// </summary>
    public class KeyValueStoreService : Service
    {
        public bool HasDBMS { get; set; }
        public bool HasReplication { get; set; }
    }
}