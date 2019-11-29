using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace backend.Repositories
{
    public class DirectAttachedStorageServiceRepository
    {
        private BrokerContext _Ctx;
        public DirectAttachedStorageServiceRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns all direct attached storage services from the database
        /// </summary>
        /// <returns>list of database services</returns>
        public List<DirectAttachedStorageService> GetDirectAttachedStorageServices()
        {
            return _Ctx.DirectAttachedStorageService.ToList();
        }
        /// <summary>
        /// the method returns a direct attached storage service from the database by id
        /// </summary>
        /// <returns> a specific direct attached storage service</returns>
        public DirectAttachedStorageService GetDirectAttachedStorageService(int id)
        {
            return _Ctx.DirectAttachedStorageService.Find(id);
        }
        /// <summary>
        /// the method posts a new direct attached storage service to the database
        /// </summary>
        /// <returns>the posted direct attached storage service</returns>
        public DirectAttachedStorageService PostDirectAttachedStorageService(DirectAttachedStorageService DirectAttachedStorageService)
        {
            _Ctx.DirectAttachedStorageService.Add(DirectAttachedStorageService);
            _Ctx.SaveChanges();
            return DirectAttachedStorageService;
        }
        /// <summary>
        /// the method puts a new direct attached storage service from the database
        /// </summary>
        /// <returns>the puted direct attached storage service</returns>
        public DirectAttachedStorageService PutDirectAttachedStorageService(DirectAttachedStorageService Service)
        {
            _Ctx.Entry(Service).State = System.Data.Entity.EntityState.Modified;
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method deletes a direct attached storage service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteDirectAttachedStorageService(int id)
        {
            DirectAttachedStorageService Service = _Ctx.DirectAttachedStorageService.Find(id);
            _Ctx.DirectAttachedStorageService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for block level storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public DirectAttachedStorageService Search(SearchVector Search)
        {
            var rand = new Random();
            var services = _Ctx.DirectAttachedStorageService.ToList();
            if (services.Count == 0) return null;
            return services[rand.Next(services.Count)];
        }
    }
}