using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains all methods to manipulate the key value store services of the database
    /// </summary>
    public class KeyValueStoreServiceRepository : GenericServiceRepository
    {

        public KeyValueStoreServiceRepository() { }

        /// <summary>
        /// the method returns all key value store services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public List<KeyValueStorageService> GetKeyValueStoreServices()
        {
            return _Ctx.KeyValueStoreService.ToList();
        }
        /// <summary>
        /// the method returns a key value store service from the database by id
        /// </summary>
        /// <returns> a specific key value store service</returns>
        public KeyValueStorageService GetKeyValueStoreService(int id)
        {
            return _Ctx.KeyValueStoreService.Find(id);
        }
        /// <summary>
        /// the method posts a new key value store service to the database
        /// </summary>
        /// <returns>the posted key value store service</returns>
        public KeyValueStorageService PostKeyValueStoreService(KeyValueStorageService Service)
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
        public ResponseWrapper<KeyValueStorageService> PutKeyValueStoreService(KeyValueStorageService Service)
        {
            base.validateNMRelations(Service);
            KeyValueStorageService OldService = _Ctx.KeyValueStoreService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<KeyValueStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            base.overwriteService(OldService, Service);

            OldService.HasDBMS = Service.HasDBMS;
            OldService.HasReplication = Service.HasReplication;

            _Ctx.SaveChanges();

            return new ResponseWrapper<KeyValueStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
        }
        /// <summary>
        /// the method deletes a key value store service from the database by id
        /// </summary>
        /// <returns>list of services</returns>
        public bool DeleteKeyValueStoreService(int id)
        {
            KeyValueStorageService Service = _Ctx.KeyValueStoreService.Find(id);
            _Ctx.KeyValueStoreService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for key value storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public ResponseWrapper<List<MatchingResponse>> Search(SearchVector Search)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponse>>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponse>();
            foreach (KeyValueStorageService Service in _Ctx.KeyValueStoreService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if (((result.points / Search.total) * 100) >= Search.minFulfillmentPercentage)
                {
                    output.Add(result);
                }
            }
            return new ResponseWrapper<List<MatchingResponse>>
            {
                state = System.Net.HttpStatusCode.OK,
                content = output
            };
        }
    }
}