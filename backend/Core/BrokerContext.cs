using backend.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace backend.Core
{
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }
    public class ApplicationUserRole : IdentityUserRole<Guid> { }
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
        /// the direct attached storage service relation
        /// </summary>
        public DbSet<DirectAttachedStorageService> DirectAttachedStorageService { get; set; }
        /// <summary>
        /// the key value store service relation
        /// </summary>
        public DbSet<KeyValueStoreService> KeyValueStoreService { get; set; }
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
        public DbSet<RelationalDatabaseService> RelationalDatabaseService { get; set; }
        /// <summary>
        /// the service relation
        /// </summary>
        public DbSet<Service> Service { get; set; }
        /// <summary>
        /// the service certificate relation
        /// </summary>
        public DbSet<ServiceCertificate> ServiceCertificate { get; set; }
        /// <summary>
        /// the service charging model relation
        /// </summary>
        public DbSet<ServiceChargingModel> ServiceChargingModel { get; set; }
        /// <summary>
        /// the service data location relation
        /// </summary>
        public DbSet<ServiceDataLocation> ServiceDataLocation { get; set; }
        /// <summary>
        /// the rule relation
        /// </summary>
        public DbSet<Rule> Rule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}