using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    public class ProjectRepository
    {
        private BrokerContext _Ctx;

        public ProjectRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Project> GetProjects()
        {
            return _Ctx.Project.ToList();
        }

        /// <summary>
        /// the method returns all projects of the current user
        /// </summary>
        /// <param name="username">current user's name</param>
        /// <returns>list of projects</returns>
        public ResponseWrapper<List<Project>> GetCurrentProjects(string username)
        {
            ApplicationUser User = _Ctx.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (User == null) return new ResponseWrapper<List<Project>>
            {
                state = HttpStatusCode.Unauthorized,
                error = "Fehler: aktueller Benutzer konnte nicht im Kontext gefunden werden"
            };
            return new ResponseWrapper<List<Project>>
            {
                state = HttpStatusCode.OK,
                content = _Ctx.Project.ToList()
            };
        }

        /// <summary>
        /// the method persists new project
        /// </summary>
        /// <param name="username">current user's name</param>
        /// <param name="Project">new project</param>
        /// <returns></returns>
        public ResponseWrapper<Project> PostCurrentProject(string username, Project Project)
        {
            ApplicationUser User = _Ctx.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (User == null) return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.Unauthorized,
                error = "Fehler: aktueller Benutzer konnte nicht im Kontext gefunden werden"
            };
            Project.UserId = User.Id;
            _Ctx.Project.Add(Project);
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = Project
            };
        }
    }
}