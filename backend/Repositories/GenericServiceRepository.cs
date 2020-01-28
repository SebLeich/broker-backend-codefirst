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
        protected Service validateNMRelations(Service service)
        {
            List<Certificate> tempCert = new List<Certificate>();
            foreach (Certificate certificate in service.Certificates)
            {
                tempCert.Add(_Ctx.Certificate.Find(certificate.Id));
            }
            List<ChargingModel> tempChar = new List<ChargingModel>();
            foreach (ChargingModel charging in service.ChargingModels)
            {
                tempChar.Add(_Ctx.ChargingModel.Find(charging.Id));
            }
            List<DataLocation> tempDLoc = new List<DataLocation>();
            foreach (DataLocation dataLocation in service.DataLocations)
            {
                tempDLoc.Add(_Ctx.DataLocation.Find(dataLocation.Id));
            }
            List<Pricing> tempPr = new List<Pricing>();
            foreach (Pricing pricing in service.Pricing)
            {
                tempPr.Add(_Ctx.Pricing.Find(pricing.Id));
            }
            Service oldService = _Ctx.Service.Find(service.Id);
            if (oldService != null)
            {
                List<Certificate> addCert = tempCert.Except(oldService.Certificates.ToList()).ToList();
                List<Certificate> removeCert = oldService.Certificates.Except(tempCert.ToList()).ToList();
                oldService.Certificates.AddRange(addCert);
                oldService.Certificates.RemoveAll(x => removeCert.Contains(x));
                List<ChargingModel> addChar = tempChar.Except(oldService.ChargingModels.ToList()).ToList();
                List<ChargingModel> removeChar = oldService.ChargingModels.Except(tempChar.ToList()).ToList();
                oldService.ChargingModels.AddRange(addChar);
                oldService.ChargingModels.RemoveAll(x => removeChar.Contains(x));
                List<DataLocation> addDLoc = tempDLoc.Except(oldService.DataLocations.ToList()).ToList();
                List<DataLocation> removeDLoc = oldService.DataLocations.Except(tempDLoc.ToList()).ToList();
                oldService.DataLocations.AddRange(addDLoc);
                oldService.DataLocations.RemoveAll(x => removeDLoc.Contains(x));
                List<Pricing> addPr = tempPr.Except(oldService.Pricing.ToList()).ToList();
                List<Pricing> removePr = oldService.Pricing.Except(tempPr.ToList()).ToList();
                oldService.Pricing.AddRange(addPr);
                oldService.Pricing.RemoveAll(x => removePr.Contains(x));
                return oldService;
            }
            else
            {
                service.Certificates = tempCert;
                service.ChargingModels = tempChar;
                service.DataLocations = tempDLoc;
                service.Pricing = tempPr;
                return service;
            }
        }

        /// <summary>
        /// the method persists the given user's search
        /// </summary>
        /// <param name="User">current user</param>
        public void saveUserSearch(string username)
        {
            if (username == null)
            {
                _Ctx.UserSearch.Add(new UserSearch
                {
                    Time = DateTime.Now
                });
                _Ctx.SaveChanges();
            }
            else
            {
                ApplicationUser User = _Ctx.Users.Where(x => x.UserName == username).FirstOrDefault();
                if (User == null)
                {
                    _Ctx.UserSearch.Add(new UserSearch
                    {
                        Time = DateTime.Now
                    });
                    _Ctx.SaveChanges();
                }
                else
                {
                    _Ctx.UserSearch.Add(new UserSearch
                    {
                        Time = DateTime.Now,
                        User = User,
                        UserId = User.Id
                    });
                    _Ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// the method overwrites all service attributes
        /// </summary>
        /// <param name="Service">new service data</param>
        protected void overwriteService(Service OldService, Service NewService)
        {
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
            OldService.Logo = NewService.Logo;
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
            System.Diagnostics.Debugger.Break();
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
            }
            else
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