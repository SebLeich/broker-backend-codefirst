using backend.Core;
using backend.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    /// <summary>
    /// the repository provides a set of methods for user control
    /// </summary>
    public class AuthentificationRepository : IDisposable
    {
        /// <summary>
        /// the data context providing database connection
        /// </summary>
        private BrokerContext _ctx;

        /// <summary>
        /// the user manager providing identity user control
        /// </summary>
        private UserManager<ApplicationUser, Guid> _userManager;

        /// <summary>
        /// the role manager providing identity role control
        /// </summary>
        private RoleManager<ApplicationRole, Guid> _roleManager;

        /// <summary>
        /// the constructor for creating new instances
        /// </summary>
        public AuthentificationRepository()
        {
            _ctx = new BrokerContext();
            _userManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(_ctx));
            _roleManager = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(_ctx));
        }

        /// <summary>
        /// the method returns all rights for the current user
        /// </summary>
        /// <param name="username">current user name</param>
        /// <returns>list of rights</returns>
        public List<RoleRuleLink> GetCurrentRights(string username)
        {
            ApplicationUser User = _ctx.Users.FirstOrDefault(x => x.UserName == username);
            if (User == null) return null;
            List<Guid> RoleIDs = User.Roles.Select(x => x.RoleId).ToList();
            List<RoleRuleLink> Links = new List<RoleRuleLink>();
            foreach (Guid Id in RoleIDs)
            {
                ApplicationRole Role = _ctx.Roles.Find(Id);
                if (Role == null) continue;
                List<Rule> Rules = _ctx.Rule.Where(x => x.Roles.Any(e => e.Id == Id)).ToList();
                Links.Add(new RoleRuleLink()
                {
                    IsAllowed = Rules.Count > 0,
                    Rules = Rules,
                    RoleId = Role.Id,
                    Role = new RoleModel()
                    {
                        RoleName = Role.Name
                    }
                });
            }
            return Links;
        }

        /// <summary>
        /// the method enables user's to register new users
        /// </summary>
        /// <param name="userModel">user model</param>
        /// <returns>identity result</returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            _userManager.AddToRole(user.Id, "User");

            return result;
        }

        /// <summary>
        /// the method enables user's to register new roles
        /// </summary>
        /// <param name="userModel">user model</param>
        /// <returns>identity result</returns>
        public async Task<IdentityResult> RegisterRole(RoleModel roleModel)
        {
            ApplicationRole role = new ApplicationRole
            {
                Name = roleModel.RoleName
            };

            var result = await _roleManager.CreateAsync(role);

            return result;
        }

        /// <summary>
        /// the method enables user's to search for users by username and password
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <returns>user</returns>
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public RoleRuleLink AllowRole(RoleRuleLink link)
        {
            
            return link;
        }

        /// <summary>
        /// the method destroys the context if needed
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
            _roleManager.Dispose();
        }

        /// <summary>
        /// return a client by id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        /// <summary>
        /// add a refresh token to the database
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }
        
        /// <summary>
        /// remove a refresh token from the database
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        /// <summary>
        /// remove a refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// find a refresh token by id
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        /// <summary>
        /// return all refresh tokens
        /// </summary>
        /// <returns></returns>
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }
    }
}