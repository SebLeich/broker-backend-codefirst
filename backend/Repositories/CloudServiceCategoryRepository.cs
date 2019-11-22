using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    /// <summary>
    /// the class contains methods for cloud service category controlling
    /// </summary>
    public class CloudServiceCategoryRepository
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
        private BrokerContext _Ctx;
        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public CloudServiceCategoryRepository()
        {
            _Ctx = new BrokerContext();
        }
        /// <summary>
        /// the method returns all cloud service categories from the database
        /// </summary>
        /// <returns></returns>
        public List<CloudServiceCategory> GetCloudServiceCategories()
        {
            return _Ctx.CloudServiceCategory.ToList();
        }
    }
}