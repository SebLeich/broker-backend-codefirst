using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

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

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetStorageTypes()
        {
            return Ok(_Repo.GetStorageTypes());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
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
