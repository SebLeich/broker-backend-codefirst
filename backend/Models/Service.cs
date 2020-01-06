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
        public string ServiceAvailability { get; set; }
        public string Logo { get; set; }
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

        public virtual List<ChargingModel> ChargingModels { get; set; }
        public virtual List<Certificate> Certificates { get; set; }
        public virtual List<DataLocation> DataLocations { get; set; }
        public virtual List<Pricing> Pricing { get; set; }

        public Service()
        {
            ChargingModels = new List<ChargingModel>();
            Certificates = new List<Certificate>();
            DataLocations = new List<DataLocation>();
            Pricing = new List<Pricing>();
        }

        public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = new MatchingResponse();
            Output.Service = this;
            Output.ServiceId = Id;
            if(Search.categories != null && Search.categories.Value != null && Search.categories.Value.Count > 0 && Search.categories.Priority > 0)
            {
                if(CloudServiceCategoryId.HasValue && Search.categories.Value.Contains(CloudServiceCategoryId.Value)) Output.pointscategories = Search.categories.Priority;
            }
            if (Search.models != null && Search.models.Value != null && Search.models.Value.Count > 0 && Search.models.Priority > 0)
            {
                if (CloudServiceModelId.HasValue && Search.models.Value.Contains(CloudServiceModelId.Value)) Output.pointsmodels = Search.models.Priority;
            }
            if (Search.providers != null && Search.providers.Value != null && Search.providers.Value.Count > 0 && Search.providers.Priority > 0)
            {
                if (ProviderId.HasValue && Search.providers.Value.Contains(ProviderId.Value)) Output.pointsproviders = Search.providers.Priority;
            }
            if (Search.deploymentinfos != null && Search.deploymentinfos.Value != null && Search.deploymentinfos.Value.Count > 0 && Search.deploymentinfos.Priority > 0)
            {
                if (DeploymentInfoId.HasValue && Search.deploymentinfos.Value.Contains(DeploymentInfoId.Value)) Output.pointsdeploymentinfos = Search.deploymentinfos.Priority;
            }
            if (Search.datalocations != null && Search.datalocations.Value != null && Search.datalocations.Value.Count > 0 && Search.datalocations.Priority > 0)
            {
                if (DataLocations.Select(x => x.Id).Intersect(Search.datalocations.Value).Any()) Output.pointsdatalocations = Search.datalocations.Priority;
            }
            if (Search.certificates != null && Search.certificates.Value != null && Search.certificates.Value.Count > 0 && Search.certificates.Priority > 0)
            {
                if (Certificates.Select(x => x.Id).Intersect(Search.certificates.Value).Any()) Output.pointscertificates = Search.certificates.Priority;
            }
            return Output;
        }
    }
}