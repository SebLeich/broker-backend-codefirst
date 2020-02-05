using backend.Models;
using backend.Repositories;
using System.Web.Http;

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

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetFeatures()
        {
            return Ok(_Repo.GetFeatures());
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetFeatureById(int id)
        {
            var result = _Repo.GetFeatureById(id);
            if(result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostFeature([FromBody] Feature feature)
        {
            var result = _Repo.PostFeature(feature);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpPut]
        [Route("")]
        public IHttpActionResult PutFeature([FromBody] Feature feature)
        {
            var result = _Repo.PutFeature(feature);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
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
