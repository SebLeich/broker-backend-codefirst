using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Repositories
{
    public class DataLocationRepository
    {
        private BrokerContext _Ctx;

        public DataLocationRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<DataLocation> GetDataLocation()
        {
            return _Ctx.DataLocation.ToList();
        }

        public DataLocation PostDataLocation(DataLocation DataLocation)
        {
            _Ctx.DataLocation.Add(DataLocation);
            _Ctx.SaveChanges();
            return DataLocation;
        }
    }
}