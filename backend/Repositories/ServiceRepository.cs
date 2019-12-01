using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains method for service control
    /// </summary>
    public class ServiceRepository
    {
        /// <summary>
        /// the attributeprovides database access
        /// </summary>
        private BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance
        /// </summary>
        public ServiceRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the endpoint returns all services
        /// </summary>
        /// <returns></returns>
        public List<ServiceWrapper> GetServices()
        {
            List<ServiceWrapper> Output = new List<ServiceWrapper>();
            List<Service> Services = _Ctx.Service.ToList();
            foreach(Service Service in Services)
            {
                Output.Add(new ServiceWrapper { Id = Service.Id, Discriminator = Service.GetType().Name, ServiceName = Service.ServiceName });
            }
            return Output;
        }
    }
}