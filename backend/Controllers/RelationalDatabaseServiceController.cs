using backend.Models;
using backend.Repositories;
using System.Web.Http;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints to manipulate relational database services
    /// </summary>
    [RoutePrefix("api/relationaldatabaseservice")]
    public class RelationalDatabaseServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate relational database services
        /// </summary>
        private RelationalDatabaseServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public RelationalDatabaseServiceController()
        {
            _Repo = new RelationalDatabaseServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetRelationalDatabaseServices()
        {
            return Ok(_Repo.GetRelationalDatabaseServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetRelationalDatabaseServiceById(int id)
        {
            return Ok(_Repo.GetRelationalDatabaseService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostRelationalDatabaseServices([FromBody] RelationalDatabaseService Service)
        {
            return Ok(_Repo.PostRelationalDatabaseService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult PutRelationalDatabaseServices([FromBody] RelationalDatabaseService Service)
        {
            return Ok(_Repo.PutRelationalDatabaseService(Service));
        }
        /// <summary>
        /// the endpoint deletes the relational database with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteRelationalDatabaseServices(int id)
        {
            return Ok(_Repo.DeleteRelationalDatabaseService(id));
        }
    }
}
