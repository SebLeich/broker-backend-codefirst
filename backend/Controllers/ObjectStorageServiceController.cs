﻿using backend.Models;
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
    /// the controller provides endpoints to manipulate object storage services
    /// </summary>
    [RoutePrefix("api/objectstorageservice")]
    public class ObjectStorageServiceController : ApiController
    {
        /// <summary>
        /// the repository provides methods to manipulate object storage services
        /// </summary>
        private ObjectStorageServiceRepository _Repo;
        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public ObjectStorageServiceController()
        {
            _Repo = new ObjectStorageServiceRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetObjectStorageServices()
        {
            return Ok(_Repo.GetObjectStorageServices());
        }
        /// <summary>
        /// the method returns a service with the given id from the database
        /// </summary>
        /// <param name="id">id of the service</param>
        /// <returns>service</returns>
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetObjectStorageServiceById(int id)
        {
            return Ok(_Repo.GetObjectStorageService(id));
        }
        /// <summary>
        /// the endpoint creates a new service within the database
        /// </summary>
        /// <param name="Service">new service</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostObjectStorageServices([FromBody] ObjectStorageService Service)
        {
            return Ok(_Repo.PostObjectStorageService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [AllowAnonymous]
        public IHttpActionResult PutObjectStorageServices([FromBody] ObjectStorageService Service)
        {
            return Ok(_Repo.PutObjectStorageService(Service));
        }
        /// <summary>
        /// the endpoint deletes the object storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [AllowAnonymous]
        public IHttpActionResult DeleteObjectStorageServices(int id)
        {
            return Ok(_Repo.DeleteObjectStorageService(id));
        }
    }
}
