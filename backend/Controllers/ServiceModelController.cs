using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// the endpoint returns all service models
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<CloudServiceModel>))]
        public IHttpActionResult GetServiceModels()
        {
            return Ok(_Repo.GetServiceModels());
        }
        /// <summary>
        /// the endpoint saves a new cloud service model to the database
        /// </summary>
        /// <param name="ServiceModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(CloudServiceModel))]
        public IHttpActionResult PostServiceModel([FromBody] CloudServiceModel ServiceModel)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostServiceModel(ServiceModel));
        }
    }
}
