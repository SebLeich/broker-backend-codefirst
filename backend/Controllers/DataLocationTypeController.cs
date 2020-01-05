using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/datalocationtype")]
    public class DataLocationTypeController : ApiController
    {
        private DataLocationTypeRepository _Repo;
        private RoleRightRepository _SecRepo;

        public DataLocationTypeController()
        {
            _Repo = new DataLocationTypeRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDataLocationypes()
        {
            return Ok(_Repo.GetDataLocationTypes());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostDataLocationType([FromBody] DataLocationType DataLocationType)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostDataLocationType(DataLocationType));
        }
    }
}
