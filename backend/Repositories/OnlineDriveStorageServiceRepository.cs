using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class OnlineDriveStorageServiceRepository
    {
        private BrokerContext _Ctx;

        public OnlineDriveStorageServiceRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the method returns all online drive storage services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public List<OnlineDriveStorageService> GetOnlineDriveStorageServices()
        {
            return _Ctx.OnlineDriveStorageService.ToList();
        }
        /// <summary>
        /// the method returns a online drive storage service from the database by id
        /// </summary>
        /// <returns> a specific online drive storage service</returns>
        public OnlineDriveStorageService GetOnlineDriveStorageService(int id)
        {
            return _Ctx.OnlineDriveStorageService.Find(id);
        }
        /// <summary>
        /// the method posts a new online drive storage service to the database
        /// </summary>
        /// <returns>the posted online drive storage service</returns>
        public OnlineDriveStorageService PostOnlineDriveStorageService(OnlineDriveStorageService Service)
        {
            Service.Creation = DateTime.Now;
            Service.LastModified = DateTime.Now;
            _Ctx.OnlineDriveStorageService.Add(Service);
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method puts a new online drive storage services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public OnlineDriveStorageService PutOnlineDriveStorageService(OnlineDriveStorageService Service)
        {
            OnlineDriveStorageService OldService = _Ctx.OnlineDriveStorageService.Find(Service.Id);
            if (OldService == null) return null;
            OldService.CloudServiceCategoryId = Service.CloudServiceCategoryId;
            OldService.CloudServiceModelId = Service.CloudServiceModelId;
            OldService.DeploymentInfoId = Service.DeploymentInfoId;
            OldService.HasAutomatedSynchronisation = Service.HasAutomatedSynchronisation;
            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasFilePermissions = Service.HasFilePermissions;
            OldService.HasFileVersioning = Service.HasFileVersioning;
            OldService.LastModified = DateTime.Now;
            OldService.ProviderId = Service.ProviderId;
            OldService.ServcieAvailability = Service.ServcieAvailability;
            OldService.ServiceCompliance = Service.ServiceCompliance;
            OldService.ServiceDescription = Service.ServiceDescription;
            OldService.ServiceName = Service.ServiceName;
            OldService.ServiceSLA = Service.ServiceSLA;
            OldService.ServiceTitle = Service.ServiceTitle;
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method deletes a online drive storage service from the database by id
        /// </summary>
        /// <returns>list of services</returns>
        public bool DeleteOnlineDriveStorageService(int id)
        {
            OnlineDriveStorageService Service = _Ctx.OnlineDriveStorageService.Find(id);
            _Ctx.OnlineDriveStorageService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for block level storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public OnlineDriveStorageService Search(SearchVector Search)
        {
            var rand = new Random();
            var services = _Ctx.OnlineDriveStorageService.ToList();
            if (services.Count == 0) return null;
            return services[rand.Next(services.Count)];
        }
    }
}