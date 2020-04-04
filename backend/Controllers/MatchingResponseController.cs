using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// <summary>
        /// this endpoint returns all matching resonses
        /// </summary>
        /// <returns>HTTP Response</returns>
        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(List<MatchingResponse>))]
        public IHttpActionResult GetMatchingResponses()
        {
            ResponseWrapper<List<MatchingResponse>> result = _Repo.GetMatchingResponses();
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
        /// <summary>
        /// this endpoint returns a specfic matching response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(MatchingResponse))]
        public IHttpActionResult GetMatchingResponseById(int id)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.GetMatchingResponseById(id);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
        /// <summary>
        /// this endpoint posts a new matching response to the data base
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(MatchingResponse))]
        public IHttpActionResult PostMatchingResponse([FromBody] MatchingResponse response)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.PostMatchingResponse(response);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
        /// <summary>
        /// this endpoint updates matching response in the data base
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(MatchingResponse))]
        public IHttpActionResult PutMatchingResponse([FromBody] MatchingResponse response)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.PutMatchingResponse(response);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
        /// <summary>
        /// this endpoint deletes a matching response from the data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(MatchingResponse))]
        public IHttpActionResult DeleteMatchingResponse(int id)
        {
            ResponseWrapper<MatchingResponse> result = _Repo.DeleteMatchingResponse(id);
            if (result.error != null) return Content(result.state, result.error);
            return Content(result.state, result.content);
        }
    }
}
