using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class StorageTypeRepository
    {
        private BrokerContext _Ctx;

        public StorageTypeRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<StorageType> GetStorageTypes()
        {
            return _Ctx.StorageType.ToList();
        }

        public StorageType PostStorageType(StorageType StorageType)
        {
            _Ctx.StorageType.Add(StorageType);
            _Ctx.SaveChanges();
            return StorageType;
        }
    }
}