using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace backend.Repositories
{
    public class ObjectStorageServiceRepository
    {
        private BrokerContext _Ctx;
        public ObjectStorageServiceRepository()
        {
            _Ctx = new BrokerContext();
        }
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
        public ObjectStorageService PutObjectStorageService(ObjectStorageService Service)
        {
            ObjectStorageService OldService = _Ctx.ObjectStorageService.Find(Service.Id);
            if (OldService == null) return null;
            OldService.CloudServiceCategoryId = Service.CloudServiceCategoryId;
            OldService.CloudServiceModelId = Service.CloudServiceModelId;
            OldService.DeploymentInfoId = Service.DeploymentInfoId;
            OldService.HasFileEncryption = Service.HasFileEncryption;
            OldService.HasFileLocking = Service.HasFileLocking;
            OldService.HasFilePermissions = Service.HasFilePermissions;
            OldService.HasFileVersioning = Service.HasFileVersioning;
            OldService.HasReplication = Service.HasReplication;
            OldService.LastModified = DateTime.Now;
            OldService.ProviderId = Service.ProviderId;
            OldService.ServcieAvailability = Service.ServcieAvailability;
            OldService.ServiceCompliance = Service.ServiceCompliance;
            OldService.ServiceDescription = Service.ServiceDescription;
            OldService.ServiceName = Service.ServiceName;
            OldService.ServiceSLA = Service.ServiceSLA;
            OldService.ServiceTitle = Service.ServiceTitle;
            foreach (ServiceCertificate C in Service.ServiceCertificates)
            {
                Certificate Ct = _Ctx.Certificate.Find(C.CertificateId);
                C.Certificate = Ct;
                _Ctx.Entry(Ct).State = EntityState.Modified;
            }
            _Ctx.SaveChanges();
            return Service;
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
        public ResponseWrapper<List<MatchingResponseWrapper<ObjectStorageService>>> Search(SearchVector Search)
        {
            if (Search.total == 0) return new ResponseWrapper<List<MatchingResponseWrapper<ObjectStorageService>>>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "Fehlerhafte Eingabe: vergebenes Rating muss ingesamt mindestens 1 sein. Wert: 0"
            };
            var output = new List<MatchingResponseWrapper<ObjectStorageService>>();
            foreach (ObjectStorageService Service in _Ctx.ObjectStorageService.ToList())
            {
                var result = Service.MatchWithSearchVector(Search);
                if (result.percentage >= Search.minFulfillmentPercentage)
                {
                    output.Add(new MatchingResponseWrapper<ObjectStorageService>
                    {
                        content = Service,
                        match = result
                    });
                }
            }
            return new ResponseWrapper<List<MatchingResponseWrapper<ObjectStorageService>>>
            {
                state = System.Net.HttpStatusCode.OK,
                content = output
            };
        }
    }
}