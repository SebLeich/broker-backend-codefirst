using backend.Models;
using backend.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace backend.Controllers
{
    /// <summary>
    /// the controller provides endpoints for account management
    /// </summary>
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// the repository provides methods for account management
        /// </summary>
        private AuthentificationRepository _Repo;
        /// <summary>
        /// the repository provides methods for role security validation
        /// </summary>
        private RoleRightRepository _SecRepo;

        /// <summary>
        /// the constructor creates a new instance of a controller
        /// </summary>
        public AccountController()
        {
            _Repo = new AuthentificationRepository();
            _SecRepo = new RoleRightRepository();
        }

        /// <summary>
        /// the endpoint returns all rights of an user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("current-rights")]
        [HttpGet]
        public IHttpActionResult GetCurrentRights()
        {
            return Ok(_Repo.GetCurrentRights(User.Identity.Name));
        }

        /// <summary>
        /// the endpoint registeres a new role in the backend
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role/register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_SecRepo.IsAllowed(User.Identity.Name, "register-roles"))
            {
                return Unauthorized();
            }

            IdentityResult result = await _Repo.RegisterRole(roleModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        /// <summary>
        /// the endpoint registeres a new user in the backend
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>HTTP Status Code</returns>
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _Repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        /// <summary>
        /// the endpoint enables the controller to dispose the controller
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Repo.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// the method returns an error result for the given response
        /// </summary>
        /// <param name="result"></param>
        /// <returns>HTTP Status Code</returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
