using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/cloudservicecategory")]
    public class CloudServiceCategoryController : ApiController
    {
        private CloudServiceCategoryRepository _Repo;
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public CloudServiceCategoryController()
        {
            _Repo = new CloudServiceCategoryRepository();
            _SecRepo = new RoleRightRepository();
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
            return Ok(_Repo.GetCloudServiceCategories());
        }
        /// <summary>
        /// the method persists a new category 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostCloudServiceCategories([FromBody] CloudServiceCategory Category)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostCloudServiceCategories(Category));
        }
    }
}
