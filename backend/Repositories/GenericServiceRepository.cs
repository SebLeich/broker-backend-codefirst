using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class GenericServiceRepository
    {
        /// <summary>
        /// the attributeprovides database access
        /// </summary>
        protected BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public GenericServiceRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the method validates the n:m relations of the given entity
        /// all passed connections will be added, all leaved connections will be removed (in case they are stored before)
        /// </summary>
        protected Service validateNMRelations(Service Service)
        {
            Service = validateCertificates(Service);
            Service = validateChargingModels(Service);
            Service = validateDataLocations(Service);
            Service = validatePricing(Service);
            return Service;
        }

        /// <summary>
        /// the method overwrites all service attributes
        /// </summary>
        /// <param name="Service">new service data</param>
        protected void overwriteService(Service OldService, Service NewService)
        {
            OldService.CloudServiceCategoryId = NewService.CloudServiceCategoryId;
            OldService.CloudServiceModelId = NewService.CloudServiceModelId;
            OldService.DeploymentInfoId = NewService.DeploymentInfoId;
            OldService.LastModified = DateTime.Now;
            OldService.ProviderId = NewService.ProviderId;
            OldService.ServiceAvailability = NewService.ServiceAvailability;
            OldService.ServiceCompliance = NewService.ServiceCompliance;
            OldService.ServiceDescription = NewService.ServiceDescription;
            OldService.ServiceName = NewService.ServiceName;
            OldService.ServiceSLA = NewService.ServiceSLA;
            OldService.ServiceTitle = NewService.ServiceTitle;
        }

        /// <summary>
        /// the method validates all certificates for the given service
        /// </summary>
        /// <param name="Service">service of validation</param>
        private Service validateCertificates(Service NewService)
        {
            List<Certificate> temp = new List<Certificate>();
            foreach (Certificate certificate in NewService.Certificates)
            {
                temp.Add(_Ctx.Certificate.Find(certificate.Id));
            }
            Service Service = _Ctx.Service.Find(NewService.Id);
            if(Service != null)
            {
                List<Certificate> add = temp.Except(Service.Certificates.ToList()).ToList();
                List<Certificate> remove = Service.Certificates.Except(temp.ToList()).ToList();
                Service.Certificates.AddRange(add);
                Service.Certificates.RemoveAll(x => remove.Contains(x));
                return Service;
            } else
            {
                NewService.Certificates = temp;
                return NewService;
            }
        }

        /// <summary>
        /// the method validates all charging models for the given service
        /// </summary>
        /// <param name="Service">service of validation</param>
        private Service validateChargingModels(Service NewService)
        {
            List<ChargingModel> temp = new List<ChargingModel>();
            foreach (ChargingModel chargingModel in NewService.ChargingModels)
            {
                temp.Add(_Ctx.ChargingModel.Find(chargingModel.Id));
            }
            Service Service = _Ctx.Service.Find(NewService.Id);
            if (Service != null)
            {
                List<ChargingModel> add = temp.Except(Service.ChargingModels.ToList()).ToList();
                List<ChargingModel> remove = Service.ChargingModels.Except(temp.ToList()).ToList();
                Service.ChargingModels.AddRange(add);
                Service.ChargingModels.RemoveAll(x => remove.Contains(x));
                return Service;
            } else
            {
                NewService.ChargingModels = temp;
                return NewService;
            }
        }

        /// <summary>
        /// the method validates all datalocations for the given service
        /// </summary>
        /// <param name="Service">service of validation</param>
        private Service validateDataLocations(Service NewService)
        {
            List<DataLocation> temp = new List<DataLocation>();
            foreach (DataLocation dataLocation in NewService.DataLocations)
            {
                temp.Add(_Ctx.DataLocation.Find(dataLocation.Id));
            }
            Service Service = _Ctx.Service.Find(NewService.Id);
            if (Service != null)
            {
                List<DataLocation> add = temp.Except(Service.DataLocations.ToList()).ToList();
                List<DataLocation> remove = Service.DataLocations.Except(temp.ToList()).ToList();
                Service.DataLocations.AddRange(add);
                Service.DataLocations.RemoveAll(x => remove.Contains(x));
                return Service;
            } else
            {
                NewService.DataLocations = temp;
                return NewService;
            } 
        }

        /// <summary>
        /// the method validates all pricing models for the given service
        /// </summary>
        /// <param name="Service">service of validation</param>
        private Service validatePricing(Service NewService)
        {
            List<Pricing> temp = new List<Pricing>();
            foreach (Pricing pricing in NewService.Pricing)
            {
                temp.Add(_Ctx.Pricing.Find(pricing.Id));
            }
            Service Service = _Ctx.Service.Find(NewService.Id);
            if (Service != null)
            {
                List<Pricing> add = temp.Except(Service.Pricing.ToList()).ToList();
                List<Pricing> remove = Service.Pricing.Except(temp.ToList()).ToList();
                Service.Pricing.AddRange(add);
                Service.Pricing.RemoveAll(x => remove.Contains(x));
                return Service;
            } else
            {
                NewService.Pricing = temp;
                return NewService;
            }
        }
    }
}