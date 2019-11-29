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
            _Ctx.Entry(Service).State = System.Data.Entity.EntityState.Modified;
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
        public BlockStorageService Search(SearchVector Search)
        {
            var rand = new Random();
            var services = _Ctx.BlockStorageService.ToList();
            if (services.Count == 0) return null;
            return services[rand.Next(services.Count)];
        }
    }
}