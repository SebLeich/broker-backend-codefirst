using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual Service Service { get; set; }
        public virtual DataLocation DataLocation { get; set; }
    }
}