﻿using backend.Models;
using backend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    [RoutePrefix("api/certificate")]
    public class CertificateController : ApiController
    {
        private CertificateRepository _Repo;
        private RoleRightRepository _SecRepo;

        public CertificateController()
        {
            _Repo = new CertificateRepository();
            _SecRepo = new RoleRightRepository();
        }
        ///<summary> 
        ///the endpoint return all certificates
        ///</summary>
        /// <returns>
        /// 
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Certificate>))]
        public IHttpActionResult GetDeploymentInfo()
        {
            return Ok(_Repo.GetCertificates());
        }

        ///<summary> 
        ///the endpoint posts a new certificate to the database
        ///</summary>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult PostDeploymentInfo([FromBody] Certificate Certficate)
        {
            if (!_SecRepo.IsAllowed(User.Identity.Name, "create-services"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            return Ok(_Repo.PostCertificate(Certficate));
        }
    }
}
