using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class OnlineDriveStorageServiceRepository : GenericServiceRepository
    {

        public OnlineDriveStorageServiceRepository() { }

        /// <summary>
        /// the method returns all online drive storage services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public List<OnlineDriveStorageService> GetOnlineDriveStorageServices()
        {
            return _Ctx.OnlineDriveStorageService.ToList();
        }
        /// <summary>
        /// the method returns a online drive storage service from the database by id
        /// </summary>
        /// <returns> a specific online drive storage service</returns>
        public OnlineDriveStorageService GetOnlineDriveStorageService(int id)
        {
            return _Ctx.OnlineDriveStorageService.Find(id);
        }
        /// <summary>
        /// the method posts a new online drive storage service to the database
        /// </summary>
        /// <returns>the posted online drive storage service</returns>
        public OnlineDriveStorageService PostOnlineDriveStorageService(OnlineDriveStorageService Service)
        {
            Service.Creation = DateTime.Now;
            Service.LastModified = DateTime.Now;
            _Ctx.OnlineDriveStorageService.Add(Service);
            _Ctx.SaveChanges();
            return Service;
        }
        /// <summary>
        /// the method puts a new online drive storage services from the database
        /// </summary>
        /// <returns>list of services</returns>
        public ResponseWrapper<OnlineDriveStorageService> PutOnlineDriveStorageService(OnlineDriveStorageService Service)
        {
            base.validateNMRelations(Service);
            OnlineDriveStorageService OldService = _Ctx.OnlineDriveStorageService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<OnlineDriveStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            base.overwriteService(OldService, Service);

            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasFileVersioning = Service.HasFileVersioning;
            OldService.HasFilePermissions = Service.HasFilePermissions;
            OldService.HasAutomatedSynchronisation = Service.HasAutomatedSynchronisation;

            _Ctx.SaveChanges();

            return new ResponseWrapper<OnlineDriveStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
        }
        /// <summary>
        /// the method deletes a online drive storage service from the database by id
        /// </summary>
        /// <returns>list of services</returns>
        public bool DeleteOnlineDriveStorageService(int id)
        {
            OnlineDriveStorageService Service = _Ctx.OnlineDriveStorageService.Find(id);
            _Ctx.OnlineDriveStorageService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for online drive storages
        /// </summary>
        /// <param name="Search">search vector</param>
        /// <returns>best match</returns>
        public ResponseWrapper<List<MatchingResponseWrapper<OnlineDriveStorageService>>> Search(SearchVector Search)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponseWrapper<OnlineDriveStorageService>>>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponseWrapper<OnlineDriveStorageService>>();
            foreach (OnlineDriveStorageService Service in _Ctx.OnlineDriveStorageService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if (((result.points / Search.total) * 100) >= Search.minFulfillmentPercentage)
                {
                    output.Add(new MatchingResponseWrapper<OnlineDriveStorageService>
                    {
                        content = Service,
                        match = result
                    });
                }
            }
            return new ResponseWrapper<List<MatchingResponseWrapper<OnlineDriveStorageService>>>
            {
                state = System.Net.HttpStatusCode.OK,
                content = output
            };
        }
    }
}