using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/deploymentinformation")]
    public class DeploymentInformationController : ApiController
    {
        private DeploymentInformationRepository _Repo;
        private RoleRightRepository _SecRepo;

        public DeploymentInformationController()
        {
            _Repo = new DeploymentInformationRepository();
            _SecRepo = new RoleRightRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDeploymentInfo()
        {
            return Ok(_Repo.GetDeploymentInfo());
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostDeploymentInfo([FromBody] DeploymentInfo DeploymentInfo)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostDeploymentInfo(DeploymentInfo));
        }
    }
}
