using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace backend.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        private ProjectRepository _Repo;
        public ProjectController()
        {
            _Repo = new ProjectRepository();
        }

        /// <summary>
        /// the endpoint returns all projects of the current user
        /// </summary>
        /// <returns>list of projects</returns>
        [Authorize]
        [HttpGet]
        [Route("current")]
        public IHttpActionResult GetCurrentProjects()
        {
            ResponseWrapper<List<Project>> response = _Repo.GetCurrentProjects(User.Identity.Name);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }

        /// <summary>
        /// the endpoint saves a new project to the database
        /// </summary>
        /// <returns>persisted project</returns>
        [Authorize]
        [HttpPost]
        [Route("current")]
        public IHttpActionResult PostCurrentProject([FromBody] Project Project)
        {
            if (Project == null) return Content(System.Net.HttpStatusCode.BadRequest, "Projekt soll angelegt werden: übergebenes Objekt ist null");
            ResponseWrapper<Project> response = _Repo.PostCurrentProject(User.Identity.Name, Project);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }

        /// <summary>
        /// the endpoint saves a set of matching responses
        /// </summary>
        /// <returns>persisted project</returns>
        [Authorize]
        [HttpPost]
        [Route("matchingResponses/{id}")]
        public IHttpActionResult PostMatchingResponses(int projectId, [FromBody] List<MatchingResponse> matchingResponses)
        {
            if (matchingResponses == null) return Content(System.Net.HttpStatusCode.BadRequest, "Matching soll angelegt werden: übergebenes Objekt ist null");
            ResponseWrapper<Project> response = _Repo.PostMatchingResponses(projectId, matchingResponses);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }

        /// <summary>
        /// the endpoint overwrites an existing project
        /// </summary>
        /// <returns>persisted project</returns>
        [Authorize]
        [HttpPut]
        [Route("")]
        public IHttpActionResult PutProject([FromBody] Project Project)
        {
            if (Project == null) return Content(System.Net.HttpStatusCode.BadRequest, "Projekt soll überschrieben werden: übergebenes Objekt ist null");
            ResponseWrapper<Project> response = _Repo.PutProject(Project);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }

        /// <summary>
        /// the endpoint removes a project with the given id
        /// </summary>
        /// <returns>deleted project</returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteProject(int id)
        {
            ResponseWrapper<Project> response = _Repo.DeleteProject(id);
            if (response.error != null) return Content(response.state, response.error);
            return Content(response.state, response.content);
        }
    }
}
