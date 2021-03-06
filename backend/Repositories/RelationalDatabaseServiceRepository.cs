﻿using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class RelationalDatabaseServiceRepository : GenericServiceRepository
    {
        public RelationalDatabaseServiceRepository() { }

        /// <summary>
        /// the method returns all relational database services from the database
        /// </summary>
        /// <returns>list of database services</returns>
        public List<RelationalDatabaseStorageService> GetRelationalDatabaseServices()
        {
            return _Ctx.RelationalDatabaseService.ToList();
        }
        /// <summary>
        /// the method returns a relational database service from the database by id
        /// </summary>
        /// <returns> a specific relational database service</returns>
        public RelationalDatabaseStorageService GetRelationalDatabaseService(int id)
        {
            return _Ctx.RelationalDatabaseService.Find(id);
        }
        /// <summary>
        /// the method posts a new relational database service to the database
        /// </summary>
        /// <returns>the posted relational database service</returns>
        public RelationalDatabaseStorageService PostRelationalDatabaseService(RelationalDatabaseStorageService Service)
        {
            Service.Creation = DateTime.Now;
            Service.LastModified = DateTime.Now;
            _Ctx.RelationalDatabaseService.Add(Service);
            _Ctx.SaveChanges();
            return Service;
        }

        /// <summary>
        /// the method puts a new relational database service from the database
        /// </summary>
        /// <returns>the puted relational database service</returns>
        public ResponseWrapper<RelationalDatabaseStorageService> PutRelationalDatabaseService(RelationalDatabaseStorageService Service)
        {
            validateNMRelations(Service);
            RelationalDatabaseStorageService OldService = _Ctx.RelationalDatabaseService.Find(Service.Id);

            if (OldService == null) return new ResponseWrapper<RelationalDatabaseStorageService>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler beim Speichern: Service konnte nicht gefunden werden"
            };

            overwriteService(OldService, Service);

            _Ctx.SaveChanges();

            return new ResponseWrapper<RelationalDatabaseStorageService>
            {
                state = HttpStatusCode.OK,
                content = OldService
            };
        }

        /// <summary>
        /// the method deletes a relational database service from the database by id
        /// </summary>
        /// <returns>1 = success </returns>
        public bool DeleteRelationalDatabaseService(int id)
        {
            RelationalDatabaseStorageService Service = _Ctx.RelationalDatabaseService.Find(id);
            _Ctx.RelationalDatabaseService.Remove(Service);
            return 1 == _Ctx.SaveChanges();
        }
        /// <summary>
        /// the endpoint enables users to search for relational database storages
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
            foreach (RelationalDatabaseStorageService Service in _Ctx.RelationalDatabaseService.ToList())
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