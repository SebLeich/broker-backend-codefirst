using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace backend.Repositories
{
    /// <summary>
    /// the repository contains methods for project controlling
    /// </summary>
    public class ProjectRepository
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
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
            validateNMRelations(Project);
            Project.MatchingResponse = new List<MatchingResponse>();
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
        /// the method persists a set of matching responses for the project with the given id
        /// </summary>
        /// <param name="id">id of the project</param>
        /// <param name="matchingResponses">set of matching responses</param>
        /// <returns>http response</returns>
        public ResponseWrapper<Project> PostMatchingResponses(List<MatchingResponse> matchingResponses)
        {
            if (matchingResponses.Count == 0) return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.BadRequest,
                error = "Matching für Projekt soll angelegt werden: leere Liste übergeben."
            };
            Project project = _Ctx.Project.Find(matchingResponses[0].ProjectId);
            if (project == null) return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.NotFound,
                error = "Matching für Projekt soll angelegt werden: Projekt-ID nicht vergeben."
            };
            foreach(MatchingResponse response in matchingResponses)
            {
                _Ctx.MatchingResponse.Add(response);
            }
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = _Ctx.Project.Find(matchingResponses[0].ProjectId)
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
            validateNMRelations(Project);
            Project.MatchingResponse = new List<MatchingResponse>();
            OldProject.LastModified = DateTime.Now;
            OldProject.ProjectDescription = Project.ProjectDescription;
            OldProject.ProjectTitle = Project.ProjectTitle;
            OldProject.AutomatedSynchronisationPriority = Project.AutomatedSynchronisationPriority;
            OldProject.CategoryPriority = Project.CategoryPriority;
            OldProject.CertificatePriority = Project.CertificatePriority;
            OldProject.DataLocationPriority = Project.DataLocationPriority;
            OldProject.DBMSPriority = Project.DBMSPriority;
            OldProject.DeploymentInfoPriority = Project.DeploymentInfoPriority;
            OldProject.FileCompressionPriority = Project.FileCompressionPriority;
            OldProject.FileEncryptionPriority = Project.FileEncryptionPriority;
            OldProject.FileLockingPriority = Project.FileLockingPriority;
            OldProject.FilePermissionsPriority = Project.FilePermissionsPriority;
            OldProject.FileVersioningPriority = Project.FileVersioningPriority;
            OldProject.HasAutomatedSynchronisation = Project.HasAutomatedSynchronisation;
            OldProject.HasDBMS = Project.HasDBMS;
            OldProject.HasFileCompression = Project.HasFileCompression;
            OldProject.HasFileEncryption = Project.HasFileEncryption;
            OldProject.HasFileLocking = Project.HasFileLocking;
            OldProject.HasFilePermissions = Project.HasFilePermissions;
            OldProject.HasFileReplication = Project.HasFileReplication;
            OldProject.HasFileVersioning = Project.HasFileVersioning;
            OldProject.MatchingResponse = Project.MatchingResponse;
            OldProject.ModelPriority = Project.ModelPriority;
            OldProject.ProviderPriority = Project.ProviderPriority;
            OldProject.ReplicationPriority = Project.ReplicationPriority;
            OldProject.StorageTypePriority = Project.StorageTypePriority;
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = OldProject
            };
        }

        /// <summary>
        /// the method deletes a project with the given id
        /// </summary>
        /// <param name="projectId">project's id</param>
        /// <returns>http response</returns>
        public ResponseWrapper<Project> DeleteProject(int projectId)
        {
            Project Project = _Ctx.Project.Find(projectId);
            if (Project == null) return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler: Projekt mit der ID konnte nicht gefunden werden"
            };
            _Ctx.Project.Remove(Project);
            _Ctx.SaveChanges();
            return new ResponseWrapper<Project>
            {
                state = HttpStatusCode.OK,
                content = Project
            };
        }

        /// <summary>
        /// the method validates the n:m relations of the given entity
        /// all passed connections will be added, all leaved connections will be removed (in case they are stored before)
        /// </summary>
        private void validateNMRelations(Project Project)
        {
            validateCertificates(Project);
            validateCloudServiceModels(Project);
            validateDataLocations(Project);
            validateDeploymentInfos(Project);
            validateProviders(Project);
            validateStorageTypes(Project);
        }

        /// <summary>
        /// the method validates all certificates for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateCertificates(Project NewProject)
        {
            List<Certificate> temp = new List<Certificate>();
            foreach (Certificate certificate in NewProject.Certificates)
            {
                temp.Add(_Ctx.Certificate.Find(certificate.Id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<Certificate> add = temp.Except(Project.Certificates.ToList()).ToList();
                List<Certificate> remove = Project.Certificates.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.Certificates.Add(x));
                remove.ForEach(x => Project.Certificates.Remove(x));
            } else
            {
                NewProject.Certificates = temp;
            }
        }

        /// <summary>
        /// the method validates all cloud service models for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateCloudServiceModels(Project NewProject)
        {
            List<CloudServiceModel> temp = new List<CloudServiceModel>();
            foreach (CloudServiceModel model in NewProject.CloudServiceModels)
            {
                temp.Add(_Ctx.CloudServiceModel.Find(model.Id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<CloudServiceModel> add = temp.Except(Project.CloudServiceModels.ToList()).ToList();
                List<CloudServiceModel> remove = Project.CloudServiceModels.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.CloudServiceModels.Add(x));
                remove.ForEach(x => Project.CloudServiceModels.Remove(x));
            } else
            {
                NewProject.CloudServiceModels = temp;
            }
        }

        /// <summary>
        /// the method validates all datalocations for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateDataLocations(Project NewProject)
        {
            List<DataLocation> temp = new List<DataLocation>();
            foreach (DataLocation dataLocation in NewProject.DataLocations)
            {
                temp.Add(_Ctx.DataLocation.Find(dataLocation.Id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<DataLocation> add = temp.Except(Project.DataLocations.ToList()).ToList();
                List<DataLocation> remove = Project.DataLocations.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.DataLocations.Add(x));
                remove.ForEach(x => Project.DataLocations.Remove(x));
            } else
            {
                NewProject.DataLocations = temp;
            }
        }

        /// <summary>
        /// the method validates all deploymentinfos for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateDeploymentInfos(Project NewProject)
        {
            List<DeploymentInfo> temp = new List<DeploymentInfo>();
            foreach (DeploymentInfo deploymentInfo in NewProject.DeploymentInfos)
            {
                temp.Add(_Ctx.DeploymentInfo.Find(deploymentInfo.Id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<DeploymentInfo> add = temp.Except(Project.DeploymentInfos.ToList()).ToList();
                List<DeploymentInfo> remove = Project.DeploymentInfos.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.DeploymentInfos.Add(x));
                remove.ForEach(x => Project.DeploymentInfos.Remove(x));
            } else
            {
                NewProject.DeploymentInfos = temp;
            }
        }

        /// <summary>
        /// the method validates all providers for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateProviders(Project NewProject)
        {
            List<Provider> temp = new List<Provider>();
            foreach (Provider provider in NewProject.Providers)
            {
                temp.Add(_Ctx.Provider.Find(provider.Id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<Provider> add = temp.Except(Project.Providers.ToList()).ToList();
                List<Provider> remove = Project.Providers.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.Providers.Add(x));
                remove.ForEach(x => Project.Providers.Remove(x));
            } else
            {
                NewProject.Providers = temp;
            }
        }

        /// <summary>
        /// the method validates all storage types for the given project
        /// </summary>
        /// <param name="NewProject">project of validation</param>
        private void validateStorageTypes(Project NewProject)
        {
            List<StorageType> temp = new List<StorageType>();
            foreach (StorageType type in NewProject.StorageTypes)
            {
                temp.Add(_Ctx.StorageType.Find(type.id));
            }
            Project Project = _Ctx.Project.Find(NewProject.ProjectId);
            if (Project != null)
            {
                List<StorageType> add = temp.Except(Project.StorageTypes.ToList()).ToList();
                List<StorageType> remove = Project.StorageTypes.Except(temp.ToList()).ToList();
                add.ForEach(x => Project.StorageTypes.Add(x));
                remove.ForEach(x => Project.StorageTypes.Remove(x));
            } else
            {
                NewProject.StorageTypes = temp;
            }
        }
    }
}