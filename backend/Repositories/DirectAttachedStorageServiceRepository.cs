using backend.Core;
using backend.Models;
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
            using (BrokerContext db = new BrokerContext())
            {
                db.DirectAttachedStorageService.Add(DirectAttachedStorageService);
                db.SaveChanges();
                return DirectAttachedStorageService;
            }

        }
        /// <summary>
        /// the method puts a new direct attached storage service from the database
        /// </summary>
        /// <returns>the puted direct attached storage service</returns>
        public DirectAttachedStorageService PutDirectAttachedStorageService(DirectAttachedStorageService Service)
        {
            using (BrokerContext db = new BrokerContext())
            {
                db.Entry(Service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Service;
            }
        }
        /// <summary>
        /// the method deletes a direct attached storage service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteDirectAttachedStorageService(int id)
        {
            using (BrokerContext db = new BrokerContext())
            {
                DirectAttachedStorageService Service = db.DirectAttachedStorageService.Find(id);
                db.DirectAttachedStorageService.Remove(Service);
                return 1 == db.SaveChanges();
            }
        }
    }
}