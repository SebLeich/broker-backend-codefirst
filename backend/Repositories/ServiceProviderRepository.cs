using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ServiceProviderRepository
    {
        private BrokerContext _Ctx;

        public ServiceProviderRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Provider> GetServiceProviders()
        {
            return _Ctx.Provider.ToList();
        }

        public Provider PostServiceProvider(Provider Provider)
        {
            _Ctx.Provider.Add(Provider);
            _Ctx.SaveChanges();
            return Provider;
        }
    }
}