using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/matchingresponse")]
    public class MatchingResponseController : ApiController
    {
        private MatchingResponseRepository _Repo;

        public MatchingResponseController()
        {
            _Repo = new MatchingResponseRepository();
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetMatchingResponses()
        {
            ResponseWrapper<List<MatchingResponse>> result = _Repo.GetMatchingResponses();
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetMatchingResponseById(int id)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.GetMatchingResponseById(id);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostMatchingResponse([FromBody] MatchingResponse response)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.PostMatchingResponse(response);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }

        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult PutMatchingResponse([FromBody] MatchingResponse response)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.PutMatchingResponse(response);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteMatchingResponse(int id)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.DeleteMatchingResponse(id);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
    }
}
