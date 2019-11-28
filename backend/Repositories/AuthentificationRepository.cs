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
        /// the method returns all users and their roles
        /// </summary>
        /// <returns></returns>
        public List<AccountModel> GetUsers()
        {
            List<ApplicationUser> Users = _ctx.Users.ToList();
            List<AccountModel> Output = new List<AccountModel>();
            foreach(ApplicationUser User in Users)
            {
                List<Guid> UserRoles = User.Roles.Select(x => x.RoleId).ToList();
                List<RoleModel> R = new List<RoleModel>();
                foreach (Guid RoleId in UserRoles)
                {
                    ApplicationRole Role = _roleManager.FindById(RoleId);
                    R.Add(new RoleModel { RoleName = Role.Name });
                }
                Output.Add(new AccountModel
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    Roles = R
                });
            }
            return Output;
        }

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
        public List<RoleRuleLink> GetCurrentRights(string userName)
        {
            ApplicationUser User = _ctx.Users.FirstOrDefault(x => x.UserName == userName);
            if (User == null) return null;
            return GetRights(User.Roles.Select(x => x.RoleId).ToList());
        }
        
        /// <summary>
        /// the method returns all rights for a given role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<RoleRuleLink> GetRightsForRole(string roleName)
        {
            ApplicationRole Role = _ctx.Roles.FirstOrDefault(x => x.Name == roleName);
            if (Role == null) return null;
            return GetRights(new List<Guid>() { Role.Id });
        }

        /// <summary>
        /// the method returns all rights for a list of given role ids
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<RoleRuleLink> GetRights(List<Guid> roleIds)
        {
            List<RoleRuleLink> Links = new List<RoleRuleLink>();
            foreach (Rule R in _ctx.Rule.ToList())
            {
                List<Guid> Matches = R.Roles.Select(x => x.Id).Intersect(roleIds).ToList();
                if (Matches.Count == 0)
                {
                    Links.Add(new RoleRuleLink()
                    {
                        IsAllowed = false,
                        Rule = R,
                        RuleId = R.Id,
                        Roles = new List<RoleModel>()
                    });
                }
                else
                {
                    List<RoleModel> Roles = new List<RoleModel>();
                    foreach (Guid id in Matches)
                    {
                        ApplicationRole AppRole = _ctx.Roles.Find(id);
                        if (AppRole == null) continue;
                        Roles.Add(new RoleModel { RoleName = AppRole.Name });
                    }
                    Links.Add(new RoleRuleLink()
                    {
                        IsAllowed = true,
                        Rule = R,
                        RuleId = R.Id,
                        Roles = Roles
                    });
                }
            }
            return Links;
        }

        /// <summary>
        /// the method returns all registered roles
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetRoles()
        {
            List<ApplicationRole> roles = _ctx.Roles.ToList();
            List<RoleModel> output = new List<RoleModel>();
            foreach (ApplicationRole r in roles) output.Add(new RoleModel
            {
                RoleName = r.Name
            });
            return output;
        }


        public RoleRuleLink PersistRoleRuleLink(RoleRuleLink link)
        {
            if (link.Roles.Count == 0) return null;
            ApplicationRole role = _roleManager.FindByName(link.Roles[0].RoleName);
            Rule rule = _ctx.Rule.Find(link.RuleId);
            if (link.IsAllowed)
            {
                if(!role.Rules.Any(x => x.Id == link.RuleId))
                {
                    role.Rules.Add(rule);
                    _ctx.SaveChanges();
                    return link;
                }
            } else
            {
                if (role.Rules.Any(x => x.Id == link.RuleId))
                {
                    role.Rules.Remove(rule);
                    _ctx.SaveChanges();
                    return link;
                }
            }
            return null;
        }

        /// <summary>
        /// the method removes a role with the given role name
        /// </summary>
        /// <param name="roleName">the roles name</param>
        /// <returns>role model</returns>
        public RoleModel DeleteRole(string roleName)
        {
            ApplicationRole role = _roleManager.FindByName(roleName);
            if (role == null) return null;
            _roleManager.Delete(role);
            return new RoleModel { RoleName = roleName };
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


        public AccountModel AddUserRoleConnection(UserRoleLink link)
        {
            ApplicationUser user = _userManager.FindByName(link.userName);
            if (user == null) return null;
            foreach(string RoleName in link.roles)
            {
                if (!_userManager.IsInRole(user.Id, RoleName)) _userManager.AddToRole(user.Id, RoleName);
            }
            List<RoleModel> Roles = new List<RoleModel>();
            foreach(var i in user.Roles)
            {
                ApplicationRole R = _roleManager.FindById(i.RoleId);
                Roles.Add(new RoleModel
                {
                    RoleName = R.Name
                });
            }
            return new AccountModel
            {
                Id = user.Id,
                Roles = Roles,
                UserName = user.UserName
            };
        }

        public AccountModel RemoveUserRoleConnection(UserRoleLink link)
        {
            ApplicationUser user = _userManager.FindByName(link.userName);
            if (user == null) return null;
            foreach (string RoleName in link.roles)
            {
                if (_userManager.IsInRole(user.Id, RoleName)) _userManager.RemoveFromRole(user.Id, RoleName);
            }
            List<RoleModel> Roles = new List<RoleModel>();
            foreach (var i in user.Roles)
            {
                ApplicationRole R = _roleManager.FindById(i.RoleId);
                Roles.Add(new RoleModel
                {
                    RoleName = R.Name
                });
            }
            return new AccountModel
            {
                Id = user.Id,
                Roles = Roles,
                UserName = user.UserName
            };
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