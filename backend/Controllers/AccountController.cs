using backend.Models;
using backend.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
        /// the constructor creates a new instance of a controller
        /// </summary>
        public AccountController()
        {
            _Repo = new AuthentificationRepository();
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
