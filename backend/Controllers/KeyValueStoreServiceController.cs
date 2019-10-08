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
    [RoutePrefix("api/keyvaluestoreservice")]
    public class KeyValueStoreServiceController : ApiController
    {
        private KeyValueStoreServiceRepository _Repo;

        public KeyValueStoreServiceController()
        {
            _Repo = new KeyValueStoreServiceRepository();
        }

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetKeyValueStoreServices()
        {
            return Ok(_Repo.GetKeyValueStoreServices());
        }

        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostKeyValueStoreServices([FromBody] KeyValueStoreService Service)
        {
            return Ok(_Repo.PostKeyValueStoreServices(Service));
        }

        [Route("")]
        [HttpPut]
        [AllowAnonymous]
        public IHttpActionResult PutKeyValueStoreServices([FromBody] KeyValueStoreService Service)
        {
            return Ok(_Repo.PutKeyValueStoreServices(Service));
        }
    }
}
