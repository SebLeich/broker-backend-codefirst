using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/serviceprovider")]
    public class ServiceProviderController : ApiController
    {
        private ServiceProviderRepository _Repo;
        private RoleRightRepository _SecRepo;

        public ServiceProviderController()
        {
            _Repo = new ServiceProviderRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetServiceProviders()
        {
            return Ok(_Repo.GetServiceProviders());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostServiceProvider([FromBody] Provider Provider)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostServiceProvider(Provider));
        }
    }
}
