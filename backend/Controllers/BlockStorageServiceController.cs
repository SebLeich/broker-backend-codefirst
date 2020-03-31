using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{

    /// <summary>
    /// the controller provides endpoints to manipulate block storage service services
    /// </summary>
    [RoutePrefix("api/blockstorageservice")]
    public class BlockStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate block storage service services
        /// </summary>
        private BlockStorageServiceRepository _Repo;
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public BlockStorageServiceController()
        {
            _Repo = new BlockStorageServiceRepository();
            _SecRepo = new RoleRightRepository();
        }

        /// <summary>
        /// the endpoint returns all block storage services from the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(List<BlockStorageService>))]
        public IHttpActionResult GetBlockStorageServices()
        {
            return Ok(_Repo.GetBlockStorageServices());
        }

        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(ResponseWrapper<BlockStorageService>))]
        public IHttpActionResult GetBlockStorageServiceById(int id)
        {
            var result = _Repo.GetBlockStorageService(id);
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
        [ResponseType(typeof(BlockStorageService))]
        public IHttpActionResult PostBlockStorageServices([FromBody] BlockStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services")) {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostBlockStorageService(Service));
        }

        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(ResponseWrapper<BlockStorageService>))]
        public IHttpActionResult PutBlockStorageServices([FromBody] BlockStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            ResponseWrapper<BlockStorageService> response = _Repo.PutBlockStorageService(Service);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }
        /// <summary>
        /// the endpoint deletes the block storage service with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteBlockStorageServices(int id)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "delete-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.DeleteBlockStorageService(id));
        }
        /// <summary>
        /// the endpoint returns a service according to the given search
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
