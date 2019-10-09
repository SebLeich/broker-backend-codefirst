using backend.Models;
using backend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints to manipulate direct attached storage services
    /// </summary>
    [RoutePrefix("api/directattachedstorageservice")]
    public class DirectAttachedStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate direct attached storage services
        /// </summary>
        private DirectAttachedStorageServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public DirectAttachedStorageServiceController()
        {
            _Repo = new DirectAttachedStorageServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetDirectAttachedStorageServices()
        {
            return Ok(_Repo.GetDirectAttachedStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetDirectAttachedStorageServiceById(int id)
        {
            return Ok(_Repo.GetDirectAttachedStorageService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostDirectAttachedStorageServices([FromBody] DirectAttachedStorageService Service)
        {
            return Ok(_Repo.PostDirectAttachedStorageService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [AllowAnonymous]
        public IHttpActionResult PutDirectAttachedStorageServices([FromBody] DirectAttachedStorageService Service)
        {
            return Ok(_Repo.PutDirectAttachedStorageService(Service));
        }
        /// <summary>
        /// the endpoint deletes the direct attached storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [AllowAnonymous]
        public IHttpActionResult DeleteDirectAttachedStorageServices(int id)
        {
            return Ok(_Repo.DeleteDirectAttachedStorageService(id));
        }
    }
}
