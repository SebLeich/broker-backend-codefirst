using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class BlockStorageServiceRepository : GenericServiceRepository
    {

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public BlockStorageServiceRepository() { }

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
            BlockStorageService = (BlockStorageService) validateNMRelations(BlockStorageService);
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
        public ResponseWrapper<BlockStorageService> PutBlockStorageService(BlockStorageService Service)
        {
            validateNMRelations(Service);
            BlockStorageService OldService = _Ctx.BlockStorageService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<BlockStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            base.overwriteService(OldService, Service);

            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasReplication = Service.HasReplication;
            OldService.StorageTypeId = Service.StorageTypeId;

            _Ctx.SaveChanges();

            return new ResponseWrapper<BlockStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
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
        public ResponseWrapper<List<MatchingResponse>> Search(SearchVector Search, string username)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponse>>
            {
                state = HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponse>();
            foreach(BlockStorageService Service in _Ctx.BlockStorageService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if(((result.points/Search.total)*100) >= Search.minFulfillmentPercentage)
                {
                    output.Add(result);
                }
            }
            saveUserSearch(username);
            return new ResponseWrapper<List<MatchingResponse>>
            {
                state = HttpStatusCode.OK,
                content = output
            };
        }
    }
}