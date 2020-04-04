using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// this endpoint returns all provider
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Provider>))]
        public IHttpActionResult GetProviders()
        {
            return Ok(_Repo.GetProviders());
        }
        /// <summary>
        /// this endpoint adds a new provider to the data base
        /// </summary>
        /// <param name="Provider"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Provider))]
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
