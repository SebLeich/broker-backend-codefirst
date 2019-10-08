using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace backend.Core
{
    /// <summary>
    /// the context contains the database's model
    /// </summary>
    public class BrokerContext : DbContext
    {
        /// <summary>
        /// the service relation
        /// </summary>
        public DbSet<Service> Services { get; set; }
    }
}