using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Repositories
{
    /// <summary>
    /// the class contains methods for meta data access
    /// </summary>
    public class MetaDataRepository
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
        private BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public MetaDataRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns the servers database metadata
        /// </summary>
        /// <returns>meta data</returns>
        public MetaData GetMetaData()
        {
            MetaData Output = new MetaData();
            Output.ProviderCount = _Ctx.Provider.Count();
            Output.SearchCount = _Ctx.UserSearch.Count();
            Output.ServiceCount = _Ctx.Service.Count();
            Output.Time = DateTime.Now;
            Output.UserCount = _Ctx.Users.Count();
            return Output;
        }
    }
}