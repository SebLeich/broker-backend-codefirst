using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains the service data location model
    /// </summary>
    public class ServiceDataLocation
    {
        [Key, Column(Order = 0)]
        public int ServiceId { get; set; }
        [Key, Column(Order = 1)]
        public int DataLocationId { get; set; }

        public Service Service { get; set; }
        public DataLocation DataLocation { get; set; }
    }
}