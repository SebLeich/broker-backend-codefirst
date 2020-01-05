﻿using backend.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;

namespace backend.Models
{
    public class MatchingResponse
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Note { get; set; }
        public bool IsFavored { get; set; }
        public int pointscategories { get; set; } = 0;
        public int pointscertificates { get; set; } = 0;
        public int pointsdatalocations { get; set; } = 0;
        public int pointsdeploymentinfos { get; set; } = 0;
        public int pointsmodels { get; set; } = 0;
        public int pointsproviders { get; set; } = 0;
        public int pointsstoragetype { get; set; } = 0;
        public int pointsHasFileEncryption { get; set; } = 0;
        public int pointsHasReplication { get; set; } = 0;
        public int pointsHasFilePermissions { get; set; } = 0;
        public int pointsHasFileLocking { get; set; } = 0;
        public int pointsHasFileCompression { get; set; } = 0;
        public int pointsHasDBMS { get; set; } = 0;
        public int pointsHasFileVersioning { get; set; } = 0;
        public int pointsHasAutomatedSynchronisation { get; set; } = 0;

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }

        public int points { get
            {
                return (
                    this.pointscategories +
                    this.pointscertificates +
                    this.pointsdatalocations +
                    this.pointsdeploymentinfos +
                    this.pointsHasAutomatedSynchronisation +
                    this.pointsHasDBMS +
                    this.pointsHasFileEncryption +
                    this.pointsHasFileCompression +
                    this.pointsHasFileLocking +
                    this.pointsHasFilePermissions +
                    this.pointsHasFileVersioning +
                    this.pointsHasReplication +
                    this.pointsmodels +
                    this.pointsproviders +
                    this.pointsstoragetype
                );
            }
        }

        public string ServiceType
        {
            get {
                if (Service == null) return null;
                var @switch = new Dictionary<Type, Func<string>> {
                    { typeof(BlockStorageService), () => { return "BlockStorageService"; } },
                    { typeof(DirectAttachedStorageService), () => { return "DirectAttachedStorageService"; } },
                    { typeof(KeyValueStorageService), () => { return "KeyValueStorageService"; } },
                    { typeof(ObjectStorageService), () => { return "ObjectStorageService"; } },
                    { typeof(OnlineDriveStorageService), () => { return "OnlineDriveStorageService"; } },
                    { typeof(RelationalDatabaseStorageService), () => { return "RelationalDatabaseStorageService"; } }
                };
                try
                {
                    return @switch[ObjectContext.GetObjectType(Service.GetType())]();
                } catch
                {
                    throw (new Exception(Service.GetType().Name));
                }
            }
        }
    }
}