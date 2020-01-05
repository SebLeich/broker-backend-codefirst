using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class DataLocationTypeRepository
    {
        private BrokerContext _Ctx;

        public DataLocationTypeRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<DataLocationType> GetDataLocationTypes()
        {
            return _Ctx.DataLocationType.ToList();
        }

        public DataLocationType PostDataLocationType(DataLocationType DataLocationType)
        {
            _Ctx.DataLocationType.Add(DataLocationType);
            _Ctx.SaveChanges();
            return DataLocationType;
        }
    }
}