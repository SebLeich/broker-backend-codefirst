﻿using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class DirectAttachedStorageServiceRepository : GenericServiceRepository
    {
        public DirectAttachedStorageServiceRepository() { }

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
        public ResponseWrapper<DirectAttachedStorageService> GetDirectAttachedStorageService(int id)
        {
            DirectAttachedStorageService directAttachedStorageService = _Ctx.DirectAttachedStorageService.Find(id);
            if (directAttachedStorageService == null) return new ResponseWrapper<DirectAttachedStorageService>
            {
                error = $"Fehler beim Abrufen: Direct Attached Storage Service mit der ID {directAttachedStorageService.Id} konnte nicht gefunden werden",
                state = HttpStatusCode.NotFound
            };
            return new ResponseWrapper<DirectAttachedStorageService>
            {
                content = directAttachedStorageService,
                state = HttpStatusCode.OK
            };
        }
        /// <summary>
        /// the method posts a new direct attached storage service to the database
        /// </summary>
        /// <returns>the posted direct attached storage service</returns>
        public DirectAttachedStorageService PostDirectAttachedStorageService(DirectAttachedStorageService DirectAttachedStorageService)
        {
            DirectAttachedStorageService = (DirectAttachedStorageService) validateNMRelations(DirectAttachedStorageService);
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
        public ResponseWrapper<DirectAttachedStorageService> PutDirectAttachedStorageService(DirectAttachedStorageService Service)
        {
            validateNMRelations(Service);
            DirectAttachedStorageService OldService = _Ctx.DirectAttachedStorageService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<DirectAttachedStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            overwriteService(OldService, Service);

            OldService.StorageTypeId = Service.StorageTypeId;

            _Ctx.SaveChanges();

            return new ResponseWrapper<DirectAttachedStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
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
        public ResponseWrapper<List<MatchingResponse>> Search(SearchVector Search, string username)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponse>>
            {
                state = HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponse>();
            foreach (DirectAttachedStorageService Service in _Ctx.DirectAttachedStorageService.ToList())
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