using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// the class contains the cloud service category
    /// </summary>
    public class CloudServiceCategory
    {
        [Key]
        public int Id { get; set; }
        public string CloudServiceCategoryName { get; set; }

        public virtual List<Service> Services { get; set; }
    }
}