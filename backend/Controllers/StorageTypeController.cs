using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    [RoutePrefix("api/storagetype")]
    public class StorageTypeController : ApiController
    {
        private StorageTypeRepository _Repo;
        private RoleRightRepository _SecRepo;

        public StorageTypeController()
        {
            _Repo = new StorageTypeRepository();
            _SecRepo = new RoleRightRepository();
        }
        /// <summary>
        /// the endpoint returns all storage types
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<StorageType>))]
        public IHttpActionResult GetStorageTypes()
        {
            return Ok(_Repo.GetStorageTypes());
        }
        /// <summary>
        /// the endpoint saves a new storage type to the database
        /// </summary>
        /// <param name="StorageType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(StorageType))]
        public IHttpActionResult PostStorageType([FromBody] StorageType StorageType)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostStorageType(StorageType));
        }
    }
}
