using backend.Models;
using backend.Repositories;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    /// <summary>
    /// the method provides endpoints for meta data access
    /// </summary>
    [RoutePrefix("api/metadata")]
    public class MetaDataController : ApiController
    {
        /// <summary>
        /// the repository provides methods for meta data access
        /// </summary>
        private MetaDataRepository _Repo;

        /// <summary>
        /// the constructor creates a new instance of the controller
        /// </summary>
        public MetaDataController()
        {
            _Repo = new MetaDataRepository();
        }

        /// <summary>
        /// the method returns the servers meta data
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(MetaData))]
        public IHttpActionResult GetMetaData()
        {
            return Ok(_Repo.GetMetaData());
        }
    }
}
