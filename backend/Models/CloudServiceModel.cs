using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains the cloud servive model
    /// </summary>
    public class CloudServiceModel
    {
        [Key]
        public int Id { get; set; }
        public string CloudServiceModelName { get; set; }
    }
}