using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class BlockStorageServiceRepository
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
        private BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public BlockStorageServiceRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns all block storage services from the database
        /// </summary>
        /// <returns>list of database services</returns>
        public List<BlockStorageService> GetBlockStorageServices()
        {
            return _Ctx.BlockStorageService.ToList();
        }
        /// <summary>
        /// the method returns a block storage service from the database by id
        /// </summary>
        /// <returns> a specific block storage service</returns>
        public BlockStorageService GetBlockStorageService(int id)
        {
            return _Ctx.BlockStorageService.Find(id);
        }
        /// <summary>
        /// the method posts a new block storage service to the database
        /// </summary>
        /// <returns>the posted block storage service</returns>
        public BlockStorageService PostBlockStorageService(BlockStorageService BlockStorageService)
        {
            BlockStorageService.Creation = DateTime.Now;
            BlockStorageService.LastModified = DateTime.Now;
            _Ctx.BlockStorageService.Add(BlockStorageService);
            _Ctx.SaveChanges();
            return BlockStorageService;
        }
        /// <summary>
        /// the method puts a new block storage service from the database
        /// </summary>
        /// <returns>the puted block storage service</returns>
        public BlockStorageService PutBlockStorageService(BlockStorageService Service)
        {
            BlockStorageService OldService = _Ctx.BlockStorageService.Find(Service.Id);
            if (OldService == null) return null;
            OldService.CloudServiceCategoryId = Service.CloudServiceCategoryId;
            OldService.CloudServiceModelId = Service.CloudServiceModelId;
            OldService.DeploymentInfoId = Service.DeploymentInfoId;
            OldService.HasFileEncryption = Service.HasFileEncryption;
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
        /// the method deletes a block storage service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteBlockStorageService(int id)
        {
            BlockStorageService Service = _Ctx.BlockStorageService.Find(id);
            _Ctx.BlockStorageService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for block level storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public ResponseWrapper<List<MatchingResponseWrapper<BlockStorageService>>> Search(SearchVector Search)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponseWrapper<BlockStorageService>>>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponseWrapper<BlockStorageService>>();
            foreach(BlockStorageService Service in _Ctx.BlockStorageService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if(result.percentage >= Search.minFulfillmentPercentage)
                {
                    output.Add(new MatchingResponseWrapper<BlockStorageService>
                    {
                        content = Service,
                        match = result
                    });
                }
            }
            return new ResponseWrapper<List<MatchingResponseWrapper<BlockStorageService>>>
            {
                state = System.Net.HttpStatusCode.OK,
                content = output
            };
        }
    }
}