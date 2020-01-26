using backend.Models;
using backend.Repositories;
using System.Net;
using System.Web.Http;

namespace backend.Controllers
{
    /// <summary>
    /// the controller enables user's to manage usecases
    /// </summary>
    [RoutePrefix("api/usecase")]
    public class UseCaseController : ApiController
    {
        /// <summary>
        /// the repository provides methods for usecase manipulation
        /// </summary>
        private UseCaseRepository _Repo;
        /// <summary>
        /// the repository provides methods for role right authorization
        /// </summary>
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the controller provides endpoints for usecase manipulation
        /// </summary>
        public UseCaseController()
        {
            _Repo = new UseCaseRepository();
            _SecRepo = new RoleRightRepository();
        }

        /// <summary>
        /// the endpoint returns all usecases of the database
        /// </summary>
        /// <returns>list of usecases</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllUseCases()
        {
            return Ok(_Repo.GetUseCases());
        }

        /// <summary>
        /// the method returns an usecase with the given id
        /// </summary>
        /// <param name="id">id of the usecase</param>
        /// <returns>usecase</returns>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUseCaseById([FromUri] int id)
        {
            var result = _Repo.GetUseCaseById(id);
            if(result.error == null)
            {
                return Content(result.state, result.content);
            } else
            {
                return Content(result.state, result.error);
            }
        }

        /// <summary>
        /// the method enables user's to post new usecases to the backend
        /// </summary>
        /// <param name="useCase">new usecase</param>
        /// <returns>persisted usecase</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostUseCase([FromBody] UseCase useCase)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "manage-use-cases"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var result = _Repo.PostUseCase(useCase);
            if(result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        /// <summary>
        /// the endpoint enables user's to update existing usecases
        /// </summary>
        /// <param name="useCase">usecase for update</param>
        /// <returns>updated usecase</returns>
        [Authorize]
        [HttpPut]
        [Route("")]
        public IHttpActionResult PutUseCase([FromBody] UseCase useCase)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "manage-use-cases"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var result = _Repo.PutUseCase(useCase);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }

        /// <summary>
        /// the endpoint enables user's to delete use cases
        /// </summary>
        /// <param name="id">id of the use case</param>
        /// <returns>deleted use case</returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteUseCase([FromUri] int id)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "manage-use-cases"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var result = _Repo.DeleteUseCase(id);
            if (result.error == null)
            {
                return Content(result.state, result.content);
            }
            return Content(result.state, result.error);
        }
    }
}
