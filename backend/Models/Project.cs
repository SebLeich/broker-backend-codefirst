using backend.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Project
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public int CategoryPriority { get; set; } = 0;
        public int CertificatePriority { get; set; } = 0;
        public int DataLocationPriority { get; set; } = 0;
        public int DeploymentInfoPriority { get; set; } = 0;
        public int ModelPriority { get; set; } = 0;
        public int ProviderPriority { get; set; } = 0;
        public int StorageTypePriority { get; set; } = 0;
        public int FileEncryptionPriority { get; set; } = 0;
        public bool HasFileEncryption { get; set; } = false;
        public int ReplicationPriority { get; set; } = 0;
        public bool HasFileReplication { get; set; } = false;
        public int FilePermissionsPriority { get; set; } = 0;
        public bool HasFilePermissions { get; set; } = false;
        public int FileLockingPriority { get; set; } = 0;
        public bool HasFileLocking { get; set; } = false;
        public int FileCompressionPriority { get; set; } = 0;
        public bool HasFileCompression { get; set; } = false;
        public int DBMSPriority { get; set; } = 0;
        public bool HasDBMS { get; set; } = false;
        public int FileVersioningPriority { get; set; } = 0;
        public bool HasFileVersioning { get; set; } = false;
        public int AutomatedSynchronisationPriority { get; set; } = 0;
        public bool HasAutomatedSynchronisation { get; set; } = false;

        public virtual ICollection<CloudServiceCategory> Categories { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<CloudServiceModel> CloudServiceModels { get; set; }
        public virtual ICollection<DataLocation> DataLocations { get; set; }
        public virtual ICollection<DeploymentInfo> DeploymentInfos { get; set; }
        public virtual ICollection<Provider> Providers { get; set; }
        public virtual ICollection<StorageType> StorageTypes { get; set; }
        public virtual ICollection<ProjectServiceType> ServiceTypes { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public virtual List<MatchingResponse> MatchingResponse { get; set; }

        public Project()
        {
            Categories = new List<CloudServiceCategory>();
            Certificates = new List<Certificate>();
            CloudServiceModels = new List<CloudServiceModel>();
            DataLocations = new List<DataLocation>();
            DeploymentInfos = new List<DeploymentInfo>();
            Providers = new List<Provider>();
            StorageTypes = new List<StorageType>();
            ServiceTypes = new List<ProjectServiceType>();
        }
    }
}