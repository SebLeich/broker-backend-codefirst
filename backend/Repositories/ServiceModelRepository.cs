using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ServiceModelRepository
    {
        private BrokerContext _Ctx;

        public ServiceModelRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<CloudServiceModel> GetServiceModels()
        {
            return _Ctx.CloudServiceModel.ToList();
        }

        public CloudServiceModel PostServiceModel(CloudServiceModel CloudServiceModel)
        {
            _Ctx.CloudServiceModel.Add(CloudServiceModel);
            _Ctx.SaveChanges();
            return CloudServiceModel;
        }
    }
}