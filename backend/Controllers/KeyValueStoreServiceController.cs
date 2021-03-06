﻿using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

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
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public KeyValueStoreServiceController()
        {
            _Repo = new KeyValueStoreServiceRepository();
            _SecRepo = new RoleRightRepository();
        }
        /// <summary>
        /// the endpoint returns all services of the database
        /// </summary>
        /// <returns>list of services</returns>
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(List<KeyValueStorageService>))]
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
        [ResponseType(typeof(KeyValueStorageService))]
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
        [Authorize]
        [ResponseType(typeof(KeyValueStorageService))]
        public IHttpActionResult PostKeyValueStoreServices([FromBody] KeyValueStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.PostKeyValueStoreService(Service));
        }
        /// <summary>
        /// the endpoint overwrites a service in the database with the given object
        /// </summary>
        /// <param name="Service">new service object</param>
        /// <returns>service</returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(KeyValueStorageService))]
        public IHttpActionResult PutKeyValueStoreServices([FromBody] KeyValueStorageService Service)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.PutKeyValueStoreService(Service));
        }
        /// <summary>
        /// the endpoint deletes the key value storage with the given id
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>boolean</returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteKeyValueStoreServices(int id)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "delete-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            return Ok(_Repo.DeleteKeyValueStoreService(id));
        }
        /// <summary>
        /// the endpoint returns a service according to the given search
        /// </summary>
        /// <returns>service</returns>
        [Route("search")]
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(List<MatchingResponse>))]
        public IHttpActionResult Search([FromBody] SearchVector Search)
        {
            var result = _Repo.Search(Search, User.Identity.Name);
            if (result.error != null) return Content(result.state, result.error);
            _Repo.saveUserSearch(User.Identity.Name);
            return Content(result.state, result.content);
        }
    }
}
