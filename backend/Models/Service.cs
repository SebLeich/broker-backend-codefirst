using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the generic service model
    /// </summary>
    public class Service
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceTitle { get; set; }
        public string ServcieAvailability { get; set; }
        public Nullable<int> CloudServiceCategoryId { get; set; }
        public Nullable<int> CloudServiceModelId { get; set; }
        public Nullable<int> ProviderId { get; set; }

        public CloudServiceCategory CloudServiceCategory { get; set; }
        public CloudServiceModel CloudServiceModel { get; set; }
        public Provider Provider { get; set; }
        public virtual List<ServiceChargingModel> ServiceChargingModels { get; set; }
        public virtual List<ServiceCertificate> ServiceCertificates { get; set; }
        public virtual List<ServiceDataLocation> ServiceDataLocations { get; set; }
    }
}