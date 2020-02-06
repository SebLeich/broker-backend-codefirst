using backend.Repositories;
using System.Web.Http;
using backend.Models;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

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

        [Authorize]
        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> PostImageFromFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            if(provider.Contents.Count == 0)
                return Content(HttpStatusCode.BadRequest, "no file found: post form data file");
            var file = provider.Contents[0];
            var fileBytes = await file.ReadAsByteArrayAsync();
            if (fileBytes.Length > 1000000)
                return Content(HttpStatusCode.BadRequest, "file to large: maximum 1MB");
            return Ok(_Repo.PostImageFromFile(fileBytes, file.Headers.ContentType.MediaType));
        }
    }
}
