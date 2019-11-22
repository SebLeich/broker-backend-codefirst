using backend.Repositories;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/cloudservicecategory")]
    public class CloudServiceCategoryController : ApiController
    {
        private CloudServiceCategoryRepository _repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public CloudServiceCategoryController()
        {
            _repo = new CloudServiceCategoryRepository();
        }
        /// <summary>
        /// the method returns all cloud service categories from the database
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCloudServiceCategories()
        {
            return Ok(_repo.GetCloudServiceCategories());
        }
    }
}
