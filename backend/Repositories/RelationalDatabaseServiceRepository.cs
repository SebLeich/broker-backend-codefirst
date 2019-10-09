using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class RelationalDatabaseServiceRepository
    {
        private BrokerContext _Ctx;
        public RelationalDatabaseServiceRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns all relational database services from the database
        /// </summary>
        /// <returns>list of database services</returns>
        public List<RelationalDatabaseService> GetRelationalDatabaseServices()
        {
            return _Ctx.RelationalDatabaseService.ToList();
        }
        /// <summary>
        /// the method returns a relational database service from the database by id
        /// </summary>
        /// <returns> a specific relational database service</returns>
        public RelationalDatabaseService GetRelationalDatabaseService(int id)
        {
            return _Ctx.RelationalDatabaseService.Find(id);
        }
        /// <summary>
        /// the method posts a new relational database service to the database
        /// </summary>
        /// <returns>the posted relational database service</returns>
        public RelationalDatabaseService PostRelationalDatabaseService(RelationalDatabaseService relationalDatabaseService)
        {
            using (BrokerContext db = new BrokerContext())
            {
                db.RelationalDatabaseService.Add(relationalDatabaseService);
                db.SaveChanges();
                return relationalDatabaseService;
            }

        }
        /// <summary>
        /// the method puts a new relational database service from the database
        /// </summary>
        /// <returns>the puted relational database service</returns>
        public RelationalDatabaseService PutRelationalDatabaseService(RelationalDatabaseService Service)
        {
            using (BrokerContext db = new BrokerContext())
            {
                db.Entry(Service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Service;
            }
        }
        /// <summary>
        /// the method deletes a relational database service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteRelationalDatabaseService(int id)
        {
            using (BrokerContext db = new BrokerContext())
            {
                RelationalDatabaseService Service = db.RelationalDatabaseService.Find(id);
                db.RelationalDatabaseService.Remove(Service);
                return 1 == db.SaveChanges();
            }
        }
    }

  
}