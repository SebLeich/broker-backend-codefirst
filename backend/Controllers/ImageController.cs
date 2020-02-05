using backend.Repositories;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        private ImageRepository _Repo;

        public ImageController()
        {
            _Repo = new ImageRepository();
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetImages()
        {
            return Ok(_Repo.GetImages());
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetImageById(int id)
        {
            var result = _Repo.GetImageById(id);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostImage([FromBody] Image image)
        {
            var result = _Repo.PostImage(image);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpPut]
        [Route("")]
        public IHttpActionResult PutImage([FromBody] Image image)
        {
            var result = _Repo.PutImage(image);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteImage(int id)
        {
            var result = _Repo.DeleteImage(id);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }
    }
}
