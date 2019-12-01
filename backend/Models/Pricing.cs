using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Pricing
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<double> PricingValue { get; set; }
        [ForeignKey("Service")]
        public Nullable<int> ServiceId { get; set; }
        [ForeignKey("PricingModel")]
        public Nullable<int> PricingModelId { get; set; }
        [ForeignKey("PricingPeriod")]
        public Nullable<int> PricingPeriodId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(PricingModelId))]
        public virtual PricingModel PricingModel { get; set; }
        [ForeignKey(nameof(PricingPeriodId))]
        public virtual PricingPeriod PricingPeriod { get; set; }
    }
}