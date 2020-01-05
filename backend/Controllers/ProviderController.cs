using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/provider")]
    public class ProviderController : ApiController
    {
        private ProviderRepository _Repo;
        private RoleRightRepository _SecRepo;

        public ProviderController()
        {
            _Repo = new ProviderRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetProviders()
        {
            return Ok(_Repo.GetProviders());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostProvider([FromBody] Provider Provider)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostProvider(Provider));
        }
    }
}
