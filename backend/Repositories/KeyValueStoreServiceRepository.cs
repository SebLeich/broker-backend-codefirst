﻿using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains all methods to manipulate the key value store services of the database
    /// </summary>
    public class KeyValueStoreServiceRepository
    {
        private BrokerContext _Ctx;

        public KeyValueStoreServiceRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the method returns all key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public List<KeyValueStoreService> GetKeyValueStoreServices()
        {
            return _Ctx.KeyValueStoreService.ToList();
        }
        /// <summary>
        /// the method returns a key value store service from the database by id
        /// </summary>
        /// <returns> a specific key value store service</returns>
        public KeyValueStoreService GetKeyValueStoreService(int id)
        {
            return _Ctx.KeyValueStoreService.Find(id);
        }
        /// <summary>
        /// the method posts a new key value store service to the database
        /// </summary>
        /// <returns>the posted key value store service</returns>
        public KeyValueStoreService PostKeyValueStoreService(KeyValueStoreService Service)
        {
            Service.Creation = DateTime.Now;
            Service.LastModified = DateTime.Now;
            _Ctx.KeyValueStoreService.Add(Service);
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method puts a new key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public KeyValueStoreService PutKeyValueStoreService(KeyValueStoreService Service)
        {
            KeyValueStoreService OldService = _Ctx.KeyValueStoreService.Find(Service.Id);
            if (OldService == null) return null;
            OldService.CloudServiceCategoryId = Service.CloudServiceCategoryId;
            OldService.CloudServiceModelId = Service.CloudServiceModelId;
            OldService.DeploymentInfoId = Service.DeploymentInfoId;
            OldService.HasDBMS = Service.HasDBMS;
            OldService.HasReplication = Service.HasReplication;
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
        /// the method deletes a key value store service from the database by id
        /// </summary>
        /// <returns>list of services</returns>
        public bool DeleteKeyValueStoreService(int id)
        {
            KeyValueStoreService Service = _Ctx.KeyValueStoreService.Find(id);
            _Ctx.KeyValueStoreService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for block level storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public KeyValueStoreService Search(SearchVector Search)
        {
            var rand = new Random();
            var services = _Ctx.KeyValueStoreService.ToList();
            if (services.Count == 0) return null;
            return services[rand.Next(services.Count)];
        }
    }
}