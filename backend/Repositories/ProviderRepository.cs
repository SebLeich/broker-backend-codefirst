using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ProviderRepository
    {
        private BrokerContext _Ctx;

        public ProviderRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Provider> GetProviders()
        {
            return _Ctx.Provider.ToList();
        }

        public Provider PostProvider(Provider Provider)
        {
            _Ctx.Provider.Add(Provider);
            _Ctx.SaveChanges();
            return Provider;
        }
    }
}