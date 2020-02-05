using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class DataLocationType
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TypeNameDE { get; set; }
        public string TypeNameEN { get; set; }
        public string TypeNameES { get; set; }

        public ICollection<DataLocation> DataLocations { get; set; }

        public DataLocationType()
        {
            DataLocations = new List<DataLocation>();
        }
    }
}