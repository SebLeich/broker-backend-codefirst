using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        /// <summary>
        /// the controller provides endpoints to manipulate services
        /// </summary>
        private ServiceRepository _Repo;
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public ServiceController()
        {
            _Repo = new ServiceRepository();
            _SecRepo = new RoleRightRepository();
        }

        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(List<ServiceWrapper>))]
        public IHttpActionResult GetServices()
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.GetServices());
        }

        /// <summary>
        /// the method returns all service classes
        /// </summary>
        /// <returns>list of service classes</returns>
        [Route("classes")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(List<ServiceClass>))]
        public IHttpActionResult GetServiceClasses()
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "manage-use-cases"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.GetServiceClasses());
        }
    }
}
