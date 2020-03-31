using backend.Models;
using backend.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// the endpoint returns all users
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<AccountModel>))]
        public IHttpActionResult GetUsers()
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.GetUsers());
        }

        /// <summary>
        /// the endpoint removes a user from the database
        /// </summary>
        /// <param name="username"> The username of the user to be deleted from the data base</param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("{username}")]
        [HttpDelete]
        [ResponseType(typeof(AccountModel))]
        public IHttpActionResult RemoveUser(string username)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.RemoveUser(username));
        }

        /// <summary>
        /// the endpoint returns all rights of the current user
        /// </summary>
        /// <params>The user is identified by the current session token</params>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("current-rights")]
        [HttpGet]
        [ResponseType(typeof(List<RoleRuleLink>))]
        public IHttpActionResult GetCurrentRights()
        {
            return Ok(_Repo.GetCurrentRights(User.Identity.Name));
        }

        /// <summary>
        /// the endpoint returns all rights for a given role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>HTTP Status Code</returns>
        /// <returns>RoleModel</returns>
        [Authorize]
        [Route("role-right/{roleName}")]
        [HttpGet]
        [ResponseType(typeof(List<RoleRuleLink>))]
        public IHttpActionResult GetRoleRights(string roleName)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.GetRightsForRole(roleName));
        }

        /// <summary>
        /// the endpoint adds a new rule to role
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role-right")]
        [HttpPost]
        [ResponseType(typeof(RoleRuleLink))]
        public IHttpActionResult PostRoleRight([FromBody] RoleRuleLink roleRuleLink)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            RoleRuleLink link = _Repo.PersistRoleRuleLink(roleRuleLink);

            if (link == null) return InternalServerError();

            return Ok(link);
        }

        /// <summary>
        /// the endpoint returns all registered roles
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role")]
        [HttpGet]
        [ResponseType(typeof(List<RoleModel>))]
        public IHttpActionResult GetRoles()
        {

            if (!_SecRepo.IsAllowed(User.Identity.Name, "register-roles"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.GetRoles());
        }

        /// <summary>
        /// the endpoint registeres a new role in the backend
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_SecRepo.IsAllowed(User.Identity.Name, "register-roles"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
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
        /// the endpoint removes a role
        /// </summary>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role/{roleName}")]
        [HttpDelete]
        [ResponseType(typeof(RoleModel))]
        public IHttpActionResult DeleteRole(string roleName)
        {

            if (!_SecRepo.IsAllowed(User.Identity.Name, "register-roles"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            RoleModel r = _Repo.DeleteRole(roleName);

            if(r == null)
            {
                return NotFound();
            }

            return Ok(r);
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
        /// the endpoint connects a user with the given roles
        /// </summary>
        /// <param name="link"></param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role-connection/add")]
        [HttpPost]
        [ResponseType(typeof(AccountModel))]
        public IHttpActionResult AddUserRoleConnection(UserRoleLink link)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.AddUserRoleConnection(link));
        }

        /// <summary>
        /// the endpoint unlinks a user with the given roles
        /// </summary>
        /// <param name="UserRolelink">
        /// An object of the type UserRoleLink
        /// </param>
        /// <returns>HTTP Status Code</returns>
        [Authorize]
        [Route("role-connection/remove")]
        [HttpPost]
        [ResponseType(typeof(AccountModel))]
        public IHttpActionResult RemoveUserRoleConnection(UserRoleLink UserRolelink)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "edit-security-guidelines"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.RemoveUserRoleConnection(UserRolelink));
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
