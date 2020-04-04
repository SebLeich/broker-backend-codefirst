using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    [RoutePrefix("api/feature")]
    public class FeatureController : ApiController
    {
        private FeatureRepository _Repo;

        public FeatureController()
        {
            _Repo = new FeatureRepository();
        }
        /// <summary>
        /// the endpoint returns als the features
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Feature>))]
        public IHttpActionResult GetFeatures()
        {
            return Ok(_Repo.GetFeatures());
        }

        /// <summary>
        /// the endpoint returns a specific feature
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Feature))]
        public IHttpActionResult GetFeatureById(int id)
        {
            var result = _Repo.GetFeatureById(id);
            if(result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }
        /// <summary>
        /// the endpoint posts a new feature to the data base
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Feature))]
        public IHttpActionResult PostFeature([FromBody] Feature feature)
        {
            var result = _Repo.PostFeature(feature);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        /// <summary>
        /// the endpoint updates an existing feature in the data base
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(Feature))]
        public IHttpActionResult PutFeature([FromBody] Feature feature)
        {
            var result = _Repo.PutFeature(feature);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        /// <summary>
        /// the endpoint deletes a feature
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(Feature))]
        public IHttpActionResult DeleteFeature(int id)
        {
            var result = _Repo.DeleteFeature(id);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }
    }
}
