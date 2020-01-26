using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class ObjectStorageServiceRepository : GenericServiceRepository
    {
        public ObjectStorageServiceRepository(){ }

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
            ObjectStorageService.Creation = DateTime.Now;
            ObjectStorageService.LastModified = DateTime.Now;
            _Ctx.ObjectStorageService.Add(ObjectStorageService);
            _Ctx.SaveChanges();
            return ObjectStorageService;
        }
        /// <summary>
        /// the method puts a new object storage service from the database
        /// </summary>
        /// <returns>the puted object storage service</returns>
        public ResponseWrapper<ObjectStorageService> PutObjectStorageService(ObjectStorageService Service)
        {
            base.validateNMRelations(Service);
            ObjectStorageService OldService = _Ctx.ObjectStorageService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<ObjectStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            base.overwriteService(OldService, Service);

            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasFileVersioning = Service.HasFileVersioning;
            OldService.HasFilePermissions = Service.HasFilePermissions;
            OldService.HasReplication = Service.HasReplication;
            OldService.HasFileLocking = Service.HasFileLocking;

            _Ctx.SaveChanges();

            return new ResponseWrapper<ObjectStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
        }
        /// <summary>
        /// the method deletes a object storage service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteObjectStorageService(int id)
        {
            ObjectStorageService Service = _Ctx.ObjectStorageService.Find(id);
            _Ctx.ObjectStorageService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for object storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public ResponseWrapper<List<MatchingResponse>> Search(SearchVector Search, string username)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponse>>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponse>();
            foreach (ObjectStorageService Service in _Ctx.ObjectStorageService.ToList())
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