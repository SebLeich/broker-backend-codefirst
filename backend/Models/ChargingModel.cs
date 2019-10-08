using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the charging model
    /// </summary>
    public class ChargingModel
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<float> Value { get; set; }
        public string Unit { get; set; }

        public ICollection<ServiceChargingModel> ServiceChargingModels { get; set; }
    }
}