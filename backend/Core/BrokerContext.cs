using backend.Models;
using System.Data.Entity;

namespace backend.Core
{
    /// <summary>
    /// the context contains the database's model
    /// </summary>
    public class BrokerContext : DbContext
    {
        /// <summary>
        /// the constructor creates a new instance of a broker context
        /// </summary>
        public BrokerContext() : base("BrokerContext")
        {

        }
        /// <summary>
        /// the certificate relation
        /// </summary>
        public DbSet<Certificate> Certificate { get; set; }
        /// <summary>
        /// the charging model relation
        /// </summary>
        public DbSet<ChargingModel> ChargingModel { get; set; }
        /// <summary>
        /// the cloud service model relation
        /// </summary>
        public DbSet<CloudServiceModel> CloudServiceModel { get; set; }
        /// <summary>
        /// the cloud service category relation
        /// </summary>
        public DbSet<CloudServiceCategory> CloudServiceCategory { get; set; }
        /// <summary>
        /// the data location
        /// </summary>
        public DbSet<DataLocation> DataLocation { get; set; }
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
        /// the service relation
        /// </summary>
        public DbSet<Service> Service { get; set; }
        /// <summary>
        /// the service charging model relation
        /// </summary>
        public DbSet<ServiceChargingModel> ServiceChargingModel { get; set; }
        /// <summary>
        /// the service certificate relation
        /// </summary>
        public DbSet<ServiceCertificate> ServiceCertificate { get; set; }
        /// <summary>
        /// the service data location relation
        /// </summary>
        public DbSet<ServiceDataLocation> ServiceDataLocation { get; set; }
    }
}