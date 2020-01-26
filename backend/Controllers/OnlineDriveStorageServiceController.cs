using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints to manipulate online drive storage services
    /// </summary>
    [RoutePrefix("api/onlinedrivestorageservice")]
    public class OnlineDriveStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate online drive storage services
        /// </summary>
        private OnlineDriveStorageServiceRepository _Repo;
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public OnlineDriveStorageServiceController()
        {
            _Repo = new OnlineDriveStorageServiceRepository();
            _SecRepo = new RoleRightRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetOnlineDriveStorageServices()
        {
            return Ok(_Repo.GetOnlineDriveStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetOnlineDriveStorageServiceById(int id)
        {
            return Ok(_Repo.GetOnlineDriveStorageService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostOnlineDriveStorageServices([FromBody] OnlineDriveStorageService Service)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var _Resp = _Repo.PostOnlineDriveStorageService(Service);
            if (_Resp == null) return NotFound();
            return Ok(_Resp);
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult PutOnlineDriveStorageServices([FromBody] OnlineDriveStorageService Service)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var _Resp = _Repo.PutOnlineDriveStorageService(Service);
            if (_Resp == null) return NotFound();
            return Ok(_Resp);
        }
        /// <summary>
        /// the endpoint deletes the online drive storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteOnlineDriveStorageServices(int id)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "delete-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.DeleteOnlineDriveStorageService(id));
        }
        /// <summary>
        /// the endpoint returns a service according to the given search
        /// </summary>
        /// <returns>service</returns>
        [Route("search")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Search([FromBody] SearchVector Search)
        {
            var result = _Repo.Search(Search, User.Identity.Name);
            if (result.error != null) return Content(result.state, result.error);
            _Repo.saveUserSearch(User.Identity.Name);
            return Content(result.state, result.content);
        }
    }
}
