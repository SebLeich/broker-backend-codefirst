using backend.Models;
using backend.Repositories;
using System.Web.Http;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints to manipulate key value storage services
    /// </summary>
    [RoutePrefix("api/keyvaluestoreservice")]
    public class KeyValueStoreServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate key value storage services
        /// </summary>
        private KeyValueStoreServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public KeyValueStoreServiceController()
        {
            _Repo = new KeyValueStoreServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetKeyValueStoreServices()
        {
            return Ok(_Repo.GetKeyValueStoreServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetKeyValueStoreServiceById(int id)
        {
            return Ok(_Repo.GetKeyValueStoreService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostKeyValueStoreServices([FromBody] KeyValueStoreService Service)
        {
            return Ok(_Repo.PostKeyValueStoreService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [AllowAnonymous]
        public IHttpActionResult PutKeyValueStoreServices([FromBody] KeyValueStoreService Service)
        {
            return Ok(_Repo.PutKeyValueStoreService(Service));
        }
        /// <summary>
        /// the endpoint deletes the key value storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [AllowAnonymous]
        public IHttpActionResult DeleteKeyValueStoreServices(int id)
        {
            return Ok(_Repo.DeleteKeyValueStoreService(id));
        }
    }
}
