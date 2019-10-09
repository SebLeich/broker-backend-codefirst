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
    /// the controller provides endpoints to manipulate online drive storage services
    /// </summary>
    [RoutePrefix("api/onlinedrivestorageservice")]
    public class OnlineDriveStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate online drive storage services
        /// </summary>
        private OnlineDriveStorageServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public OnlineDriveStorageServiceController()
        {
            _Repo = new OnlineDriveStorageServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetOnlineDriveStorageServices()
        {
            return Ok(_Repo.GetOnlineDriveStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetOnlineDriveStorageServiceById(int id)
        {
            return Ok(_Repo.GetOnlineDriveStorageService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostOnlineDriveStorageServices([FromBody] OnlineDriveStorageService Service)
        {
            return Ok(_Repo.PostOnlineDriveStorageService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [AllowAnonymous]
        public IHttpActionResult PutOnlineDriveStorageServices([FromBody] OnlineDriveStorageService Service)
        {
            return Ok(_Repo.PutOnlineDriveStorageService(Service));
        }
        /// <summary>
        /// the endpoint deletes the online drive storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [AllowAnonymous]
        public IHttpActionResult DeleteOnlineDriveStorageServices(int id)
        {
            return Ok(_Repo.DeleteOnlineDriveStorageService(id));
        }
    }
}
