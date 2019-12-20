using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/datalocation")]
    public class DataLocationController : ApiController
    {
        private DataLocationRepository _Repo;
        private RoleRightRepository _SecRepo;

        public DataLocationController()
        {
            _Repo = new DataLocationRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDataLocation()
        {
            return Ok(_Repo.GetDataLocation());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostGetDataLocation([FromBody] DataLocation DataLocation)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostDataLocation(DataLocation));
        }
    }
}