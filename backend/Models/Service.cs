using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        public DateTime Creation { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("CloudServiceCategory")]
        public Nullable<int> CloudServiceCategoryId { get; set; }
        [ForeignKey("CloudServiceModel")]
        public Nullable<int> CloudServiceModelId { get; set; }
        [ForeignKey("Provider")]
        public Nullable<int> ProviderId { get; set; }
        [ForeignKey("DeploymentInfo")]
        public Nullable<int> DeploymentInfoId { get; set; }

        [ForeignKey(nameof(CloudServiceCategoryId))]
        public virtual CloudServiceCategory CloudServiceCategory { get; set; }
        [ForeignKey(nameof(CloudServiceModelId))]
        public virtual CloudServiceModel CloudServiceModel { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public virtual Provider Provider { get; set; }
        [ForeignKey(nameof(DeploymentInfoId))]
        public virtual DeploymentInfo DeploymentInfo { get; set; }

        public virtual List<ServiceChargingModel> ServiceChargingModels { get; set; }
        public virtual List<ServiceCertificate> ServiceCertificates { get; set; }
        public virtual List<ServiceDataLocation> ServiceDataLocations { get; set; }
        public virtual List<Pricing> Pricing { get; set; }

        public Service()
        {
            ServiceChargingModels = new List<ServiceChargingModel>();
            ServiceCertificates = new List<ServiceCertificate>();
            ServiceDataLocations = new List<ServiceDataLocation>();
            Pricing = new List<Pricing>();
        }

        public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = new MatchingResponse();
            if(Search.categories != null && Search.categories.value != null && Search.categories.value.Count > 0 && Search.categories.priority > 0)
            {
                Output.prioritycategories = Search.categories.priority;
                if(CloudServiceCategoryId.HasValue && Search.categories.value.Contains(CloudServiceCategoryId.Value)) Output.pointscategories = Search.categories.priority;
            }
            if (Search.models != null && Search.models.value != null && Search.models.value.Count > 0 && Search.models.priority > 0)
            {
                Output.prioritymodels = Search.models.priority;
                if (CloudServiceModelId.HasValue && Search.models.value.Contains(CloudServiceModelId.Value)) Output.pointsmodels = Search.models.priority;
            }
            if (Search.providers != null && Search.providers.value != null && Search.providers.value.Count > 0 && Search.providers.priority > 0)
            {
                Output.priorityproviders = Search.providers.priority;
                if (ProviderId.HasValue && Search.providers.value.Contains(ProviderId.Value)) Output.pointsproviders = Search.providers.priority;
            }
            if (Search.deploymentinfos != null && Search.deploymentinfos.value != null && Search.deploymentinfos.value.Count > 0 && Search.deploymentinfos.priority > 0)
            {
                Output.prioritydeploymentinfos = Search.deploymentinfos.priority;
                if (DeploymentInfoId.HasValue && Search.deploymentinfos.value.Contains(DeploymentInfoId.Value)) Output.pointsdeploymentinfos = Search.deploymentinfos.priority;
            }
            if (Search.datalocations != null && Search.datalocations.value != null && Search.datalocations.value.Count > 0 && Search.datalocations.priority > 0)
            {
                Output.prioritydatalocations = Search.datalocations.priority;
                if (ServiceDataLocations.Select(x => x.DataLocationId).Intersect(Search.datalocations.value).Any()) Output.pointsdatalocations = Search.datalocations.priority;
            }
            if (Search.certificates != null && Search.certificates.value != null && Search.certificates.value.Count > 0 && Search.certificates.priority > 0)
            {
                Output.prioritycertificates = Search.certificates.priority;
                if (ServiceCertificates.Select(x => x.CertificateId).Intersect(Search.certificates.value).Any()) Output.pointscertificates = Search.certificates.priority;
            }
            return Output;
        }
    }
}