using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// this endpoint returns all data locations from the data base
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<DataLocation>))]
        public IHttpActionResult GetDataLocation()
        {
            return Ok(_Repo.GetDataLocation());
        }
        /// <summary>
        /// this endpoint posts a new data location to the data base
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DataLocation))]
        public IHttpActionResult PostDataLocation([FromBody] DataLocation DataLocation)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostDataLocation(DataLocation));
        }
    }
}