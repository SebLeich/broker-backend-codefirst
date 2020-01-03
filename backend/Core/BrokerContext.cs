using backend.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace backend.Core
{
    /// <summary>
    /// the broker context user class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> {
        /// <summary>
        /// the constructor creates a new instance of an application user
        /// </summary>
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }
    /// <summary>
    /// the application user roles
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<Guid> { }
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUserClaim : IdentityUserClaim<Guid> { }

    public class ApplicationUserLogin : IdentityUserLogin<Guid> { }

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole> {
        public virtual ICollection<Rule> Rules { get; set; } = new HashSet<Rule>();
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
        }
    }

    /// <summary>
    /// the context contains the database's model
    /// </summary>
    public class BrokerContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        /// <summary>
        /// the constructor creates a new instance of a broker context
        /// </summary>
        public BrokerContext() : base("BrokerContext")
        {

        }
        /// <summary>
        /// all clients registered for access
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// all refresh tokens stored
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        /// <summary>
        /// the block storage service relation
        /// </summary>
        public DbSet<BlockStorageService> BlockStorageService { get; set; }
        /// <summary>
        /// the certificate relation
        /// </summary>
        public DbSet<Certificate> Certificate { get; set; }
        /// <summary>
        /// the charging model relation
        /// </summary>
        public DbSet<ChargingModel> ChargingModel { get; set; }
        /// <summary>
        /// the cloud service category relation
        /// </summary>
        public DbSet<CloudServiceCategory> CloudServiceCategory { get; set; }
        /// <summary>
        /// the cloud service model relation
        /// </summary>
        public DbSet<CloudServiceModel> CloudServiceModel { get; set; }
        /// <summary>
        /// the data location relation
        /// </summary>
        public DbSet<DataLocation> DataLocation { get; set; }
        /// <summary>
        /// the data location type relation
        /// </summary>
        public DbSet<DataLocationType> DataLocationType { get; set; }
        /// <summary>
        /// the deployment information relation
        /// </summary>
        public DbSet<DeploymentInfo> DeploymentInfo { get; set; }
        /// <summary>
        /// the direct attached storage service relation
        /// </summary>
        public DbSet<DirectAttachedStorageService> DirectAttachedStorageService { get; set; }
        /// <summary>
        /// the key value store service relation
        /// </summary>
        public DbSet<KeyValueStorageService> KeyValueStoreService { get; set; }
        /// <summary>
        /// a set of matching responses
        /// </summary>
        public DbSet<MatchingResponse> MatchingResponse { get; set; }
        /// <summary>
        /// the online drive storage service relation
        /// </summary>
        public DbSet<ObjectStorageService> ObjectStorageService { get; set; }
        /// <summary>
        /// the object storage service relation
        /// </summary>
        public DbSet<OnlineDriveStorageService> OnlineDriveStorageService { get; set; }
        /// <summary>
        /// the payment relation
        /// </summary>
        public DbSet<Payment> Payment { get; set; }
        /// <summary>
        /// the service pricing relation
        /// </summary>
        public DbSet<Pricing> Pricing { get; set; }
        /// <summary>
        /// the pricing model relation
        /// </summary>
        public DbSet<PricingModel> PricingModel { get; set; }
        /// <summary>
        /// the pricing period relation
        /// </summary>
        public DbSet<PricingPeriod> PricingPeriod { get; set; }
        /// <summary>
        /// all projects
        /// </summary>
        public DbSet<Project> Project { get; set; }
        /// <summary>
        /// all service types
        /// </summary>
        public DbSet<ProjectServiceType> ProjectServiceType { get; set; }
        /// <summary>
        /// the provider relation
        /// </summary>
        public DbSet<Provider> Provider { get; set; }
        /// <summary>
        /// the provider payment relation
        /// </summary>
        public DbSet<ProviderPayment> ProviderPayment { get; set; }
        /// <summary>
        /// the relational database relation
        /// </summary>
        public DbSet<RelationalDatabaseStorageService> RelationalDatabaseService { get; set; }
        /// <summary>
        /// the rule relation
        /// </summary>
        public DbSet<Rule> Rule { get; set; }
        /// <summary>
        /// the service relation
        /// </summary>
        public DbSet<Service> Service { get; set; }
        /// <summary>
        /// the service storage type relation
        /// </summary>
        public DbSet<StorageType> StorageType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}