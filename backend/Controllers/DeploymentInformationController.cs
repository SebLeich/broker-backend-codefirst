using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// this endpoint returns all deployment information
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DeploymentInfo>))]
        public IHttpActionResult GetDeploymentInfo()
        {
            return Ok(_Repo.GetDeploymentInfo());
        }
        /// <summary>
        /// this endpoint adds a new deployment information to the data base
        /// </summary>
        /// <param name="DeploymentInfo"></param>
        /// <returns></returns>
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
