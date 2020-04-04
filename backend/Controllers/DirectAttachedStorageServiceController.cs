using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints to manipulate direct attached storage services
    /// </summary>
    [RoutePrefix("api/directattachedstorageservice")]
    public class DirectAttachedStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate direct attached storage services
        /// </summary>
        private DirectAttachedStorageServiceRepository _Repo;
        private RoleRightRepository _SecRepo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public DirectAttachedStorageServiceController()
        {
            _Repo = new DirectAttachedStorageServiceRepository();
            _SecRepo = new RoleRightRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(List<DirectAttachedStorageService>))]
        public IHttpActionResult GetDirectAttachedStorageServices()
        {
            return Ok(_Repo.GetDirectAttachedStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(DirectAttachedStorageService))]
        public IHttpActionResult GetDirectAttachedStorageServiceById(int id)
        {
            var result = _Repo.GetDirectAttachedStorageService(id);
            if(result.error == null)
            {
                return Content(result.state, result.content);
            } else
            {
                return Content(result.state, result.error);
            }
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(DirectAttachedStorageService))]
        public IHttpActionResult PostDirectAttachedStorageServices([FromBody] DirectAttachedStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.PostDirectAttachedStorageService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(DirectAttachedStorageService))]
        public IHttpActionResult PutDirectAttachedStorageServices([FromBody] DirectAttachedStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.PutDirectAttachedStorageService(Service));
        }
        /// <summary>
        /// the endpoint deletes the direct attached storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteDirectAttachedStorageServices(int id)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "delete-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.DeleteDirectAttachedStorageService(id));
        }
        /// <summary>
        /// the endpoint returns services according to the given search
        /// </summary>
        /// <returns>service</returns>
        [Route("search")]
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(List<MatchingResponse>))]
        public IHttpActionResult Search([FromBody] SearchVector Search)
        {
            var result = _Repo.Search(Search, User.Identity.Name);
            if (result.error != null) return Content(result.state, result.error);
            _Repo.saveUserSearch(User.Identity.Name);
            return Content(result.state, result.content);
        }
    }
}
