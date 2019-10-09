using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ObjectStorageServiceRepository
    {
        private BrokerContext _Ctx;
        public ObjectStorageServiceRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns all object storage services from the database
        /// </summary>
        /// <returns>list of database services</returns>
        public List<ObjectStorageService> GetObjectStorageServices()
        {
            return _Ctx.ObjectStorageService.ToList();
        }
        /// <summary>
        /// the method returns a object storage service from the database by id
        /// </summary>
        /// <returns> a specific object storage service</returns>
        public ObjectStorageService GetObjectStorageService(int id)
        {
            return _Ctx.ObjectStorageService.Find(id);
        }
        /// <summary>
        /// the method posts a new object storage service to the database
        /// </summary>
        /// <returns>the posted object storage service</returns>
        public ObjectStorageService PostObjectStorageService(ObjectStorageService ObjectStorageService)
        {
            using (BrokerContext db = new BrokerContext())
            {
                db.ObjectStorageService.Add(ObjectStorageService);
                db.SaveChanges();
                return ObjectStorageService;
            }

        }
        /// <summary>
        /// the method puts a new object storage service from the database
        /// </summary>
        /// <returns>the puted object storage service</returns>
        public ObjectStorageService PutObjectStorageService(ObjectStorageService Service)
        {
            using (BrokerContext db = new BrokerContext())
            {
                db.Entry(Service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Service;
            }
        }
        /// <summary>
        /// the method deletes a object storage service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteObjectStorageService(int id)
        {
            using (BrokerContext db = new BrokerContext())
            {
                ObjectStorageService Service = db.ObjectStorageService.Find(id);
                db.ObjectStorageService.Remove(Service);
                return 1 == db.SaveChanges();
            }
        }
    }
}