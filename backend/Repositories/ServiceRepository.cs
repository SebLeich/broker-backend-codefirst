using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains method for service control
    /// </summary>
    public class ServiceRepository : GenericServiceRepository
    {

        /// <summary>
        /// the constructor creates a new instance
        /// </summary>
        public ServiceRepository() { }

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
                Output.Add(new ServiceWrapper {
                    Id = Service.Id,
                    Discriminator = Service.GetType().Name,
                    ServiceName = Service.ServiceName,
                    Creation = Service.Creation,
                    LastModified = Service.LastModified
                });
            }
            return Output;
        }

        /// <summary>
        /// the method returns a list containing all service classes
        /// </summary>
        /// <returns>list of service classes</returns>
        public List<ServiceClass> GetServiceClasses()
        {
            return _Ctx.ServiceClass.ToList();
        }
    }
}