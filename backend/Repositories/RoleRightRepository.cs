using backend.Core;
using backend.Models;
using System;
using System.Linq;

namespace backend.Repositories
{
    /// <summary>
    /// the class provides roleright validation
    /// </summary>
    public class RoleRightRepository : IDisposable
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
        private BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public RoleRightRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the method checks whether the current user is allowed to 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="code"></param>
        public bool IsAllowed(string username, string code)
        {
            ApplicationUser u = _Ctx.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (u == null) return false;
            Rule r = _Ctx.Rule.Where(x => x.RuleCode == code).FirstOrDefault();
            if (r == null) return false;
            if(u.Roles.Select(x => x.RoleId).Intersect(r.Roles.Select(x => x.Id)).Any()) return true;
            return false;
        }

        /// <summary>
        /// the method destroys the current instance
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}