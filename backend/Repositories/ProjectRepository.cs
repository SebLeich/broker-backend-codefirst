using backend.Core;
using backend.Models;
using System;
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
            Project.Created = DateTime.Now;
            Project.LastModified = DateTime.Now;
            _Ctx.Project.Add(Project);
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = Project
            };
        }

        /// <summary>
        /// the method overwrites an existing project
        /// </summary>
        /// <param name="username">current user's name</param>
        /// <param name="Project">new project</param>
        /// <returns></returns>
        public ResponseWrapper<Project> PutProject(Project Project)
        {
            Project OldProject = _Ctx.Project.Find(Project.ProjectId);
            if (OldProject == null) return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler: Projekt soll beareitet werden, existiert allerdings noch nicht"
            };
            OldProject.LastModified = DateTime.Now;
            OldProject.ProjectDescription = Project.ProjectDescription;
            OldProject.ProjectTitle = Project.ProjectTitle;
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = OldProject
            };
        }
    }
}