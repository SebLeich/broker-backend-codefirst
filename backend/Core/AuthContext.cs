using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace backend.Core
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {

        }
        /// <summary>
        /// all clients registered for access
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// all refresh tokens stored
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}