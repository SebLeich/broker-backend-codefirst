using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/cloudservicemodel")]
    public class ServiceModelController : ApiController
    {
        private ServiceModelRepository _Repo;
        private RoleRightRepository _SecRepo;

        public ServiceModelController()
        {
            _Repo = new ServiceModelRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetServiceProviders()
        {
            return Ok(_Repo.GetServiceModels());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostServiceProvider([FromBody] CloudServiceModel ServiceModel)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostServiceModel(ServiceModel));
        }
    }
}
