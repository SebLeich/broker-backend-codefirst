﻿using backend.Models;
using backend.Repositories;
using System.Web.Http;

namespace backend.Controllers
{

    /// <summary>
    /// the controller provides endpoints to manipulate block storage service services
    /// </summary>
    [RoutePrefix("api/blockstorageservice")]
    public class BlockStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate block storage service services
        /// </summary>
        private BlockStorageServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public BlockStorageServiceController()
        {
            _Repo = new BlockStorageServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetBlockStorageServices()
        {
            return Ok(_Repo.GetBlockStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetBlockStorageServiceById(int id)
        {
            return Ok(_Repo.GetBlockStorageService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostBlockStorageServices([FromBody] BlockStorageService Service)
        {
            return Ok(_Repo.PostBlockStorageService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult PutBlockStorageServices([FromBody] BlockStorageService Service)
        {
            return Ok(_Repo.PutBlockStorageService(Service));
        }
        /// <summary>
        /// the endpoint deletes the block storage service with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteBlockStorageServices(int id)
        {
            return Ok(_Repo.DeleteBlockStorageService(id));
        }
    }
}
