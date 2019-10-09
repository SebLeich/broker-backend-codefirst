using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains a relational database service
    /// </summary>
    public class RelationalDatabaseService :Service
    {
        public bool HasDBMS { get; set; }
        public bool HasReplication { get; set; }
    }
}