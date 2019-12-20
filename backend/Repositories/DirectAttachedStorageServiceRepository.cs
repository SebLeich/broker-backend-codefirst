﻿using backend.Core;
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
            DirectAttachedStorageService.Creation = DateTime.Now;
            DirectAttachedStorageService.LastModified = DateTime.Now;
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
            DirectAttachedStorageService OldService = _Ctx.DirectAttachedStorageService.Find(Service.Id);
            if (OldService == null) return null;
            OldService.CloudServiceCategoryId = Service.CloudServiceCategoryId;
            OldService.CloudServiceModelId = Service.CloudServiceModelId;
            OldService.DeploymentInfoId = Service.DeploymentInfoId;
            OldService.HasFileCompression = Service.HasFileCompression;
            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasFileLocking = Service.HasFileLocking;
            OldService.HasFilePermissions = Service.HasFilePermissions;
            OldService.HasReplication = Service.HasReplication;
            OldService.LastModified = DateTime.Now;
            OldService.ProviderId = Service.ProviderId;
            OldService.ServcieAvailability = Service.ServcieAvailability;
            OldService.ServiceCompliance = Service.ServiceCompliance;
            OldService.ServiceDescription = Service.ServiceDescription;
            OldService.ServiceName = Service.ServiceName;
            OldService.ServiceSLA = Service.ServiceSLA;
            OldService.ServiceTitle = Service.ServiceTitle;
            OldService.StorageTypeId = Service.StorageTypeId;
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
        /// the endpoint enables users to search for direct attached storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public ResponseWrapper<List<MatchingResponseWrapper<DirectAttachedStorageService>>> Search(SearchVector Search)
        {
            var output = new List<MatchingResponseWrapper<DirectAttachedStorageService>>();
            foreach (DirectAttachedStorageService Service in _Ctx.DirectAttachedStorageService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if (result.total == 0) return new ResponseWrapper<List<MatchingResponseWrapper<DirectAttachedStorageService>>>
                {
                    state = System.Net.HttpStatusCode.BadRequest,
                    error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
                };
                if (result.percentage >= Search.minFulfillmentPercentage)
                {
                    output.Add(new MatchingResponseWrapper<DirectAttachedStorageService>
                    {
                        content = Service,
                        match = result
                    });
                }
            }
            return new ResponseWrapper<List<MatchingResponseWrapper<DirectAttachedStorageService>>>
            {
                state = System.Net.HttpStatusCode.OK,
                content = output
            };
        }
    }
}