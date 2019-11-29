using backend.Core;
using backend.Models;
using System;
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
            _Ctx.RelationalDatabaseService.Add(relationalDatabaseService);
            _Ctx.SaveChanges();
            return relationalDatabaseService;
        }
        /// <summary>
        /// the method puts a new relational database service from the database
        /// </summary>
        /// <returns>the puted relational database service</returns>
        public RelationalDatabaseService PutRelationalDatabaseService(RelationalDatabaseService Service)
        {
            _Ctx.Entry(Service).State = System.Data.Entity.EntityState.Modified;
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method deletes a relational database service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteRelationalDatabaseService(int id)
        {
            RelationalDatabaseService Service = _Ctx.RelationalDatabaseService.Find(id);
            _Ctx.RelationalDatabaseService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for block level storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public RelationalDatabaseService Search(SearchVector Search)
        {
            var rand = new Random();
            var services = _Ctx.RelationalDatabaseService.ToList();
            if (services.Count == 0) return null;
            return services[rand.Next(services.Count)];
        }
    }
}