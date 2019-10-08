using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains all methods to manipulate the key value store services of the database
    /// </summary>
    public class KeyValueStoreServiceRepository
    {
        /// <summary>
        /// the method returns all key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public List<KeyValueStoreService> GetKeyValueStoreServices()
        {
            using(var db = new BrokerContext())
            {
                return db.KeyValueStoreService.ToList();
            }
        }
        /// <summary>
        /// the method posts a new key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public KeyValueStoreService PostKeyValueStoreServices(KeyValueStoreService Service)
        {
            using (var db = new BrokerContext())
            {
                db.KeyValueStoreService.Add(Service);
                db.SaveChanges();
                return Service;
            }
        }
        /// <summary>
        /// the method puts a new key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public KeyValueStoreService PutKeyValueStoreServices(KeyValueStoreService Service)
        {
            using (var db = new BrokerContext())
            {
                db.Entry(Service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Service;
            }
        }
    }
}