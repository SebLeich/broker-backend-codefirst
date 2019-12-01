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
        public string ServiceCompliance { get; set; }
        public string ServiceSLA { get; set; }
        public string ServiceTitle { get; set; }
        public string ServcieAvailability { get; set; }

        [ForeignKey("CloudServiceCategory")]
        public Nullable<int> CloudServiceCategoryId { get; set; }
        [ForeignKey("CloudServiceModel")]
        public Nullable<int> CloudServiceModelId { get; set; }
        [ForeignKey("Provider")]
        public Nullable<int> ProviderId { get; set; }
        [ForeignKey("DeploymentInfo")]
        public Nullable<int> DeploymentInfoId { get; set; }
        [ForeignKey("StorageType")]
        public Nullable<int> StorageTypeId { get; set; }

        [ForeignKey(nameof(CloudServiceCategoryId))]
        public virtual CloudServiceCategory CloudServiceCategory { get; set; }
        [ForeignKey(nameof(CloudServiceModelId))]
        public virtual CloudServiceModel CloudServiceModel { get; set; }
        [ForeignKey(nameof(StorageTypeId))]
        public virtual StorageType StorageType { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual Provider Provider { get; set; }
        [ForeignKey(nameof(DeploymentInfoId))]
        public virtual DeploymentInfo DeploymentInfo { get; set; }

        public virtual List<ServiceChargingModel> ServiceChargingModels { get; set; }
        public virtual List<ServiceCertificate> ServiceCertificates { get; set; }
        public virtual List<ServiceDataLocation> ServiceDataLocations { get; set; }
        public virtual List<Pricing> Pricing { get; set; }
    }
}