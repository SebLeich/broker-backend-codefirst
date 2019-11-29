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
            _Ctx.Entry(Service).State = System.Data.Entity.EntityState.Modified;
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