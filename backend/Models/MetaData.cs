using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class MetaData
    {
        public int ServiceCount { get; set; }
        public int ProviderCount { get; set; }
        public int UserCount { get; set; }
        public int SearchCount { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        public MetaData()
        {

        }
    }
}