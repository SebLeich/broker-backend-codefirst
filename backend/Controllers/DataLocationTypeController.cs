using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// this endpoint returns all data locations types
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DataLocationType>))]
        public IHttpActionResult GetDataLocationypes()
        {
            return Ok(_Repo.GetDataLocationTypes());
        }
        /// <summary>
        /// this endpoint adds a new data locations type to the data base
        /// </summary>
        /// <param name="DataLocationType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DataLocationType))]
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
