using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        IQueryable GetProjectsWithConvocation();
        Project GetProjectWithConvocationAndCollaboratorsById(int id);
        Project GetProjectByName(string name);

        IQueryable<ProjectResponse> GetAllProjectsResponse();
        ProjectResponse GetProjectResponseById(int id);

        Task<ProjectCollaboratorsDetailTemp> AddProjectCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaboratorsDetailTemp);
        IQueryable<ProjectCollaboratorsDetailTemp> GetAllProjectCollaboratorsDetailTemps();
        ProjectCollaboratorsDetailTemp GetProjectCollaboratorsDetailTempsById(int id);
        IQueryable<ProjectCollaborator> GetProjectCollaboratorsByProjectId(int projectId);
        void DeleteCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaborator);
        Task<Project> AddCollaboratorsAsync(int id, List<ProjectCollaborator> collaborators, List<ProjectCollaboratorsDetailTemp> projectCollaboratorsTemp);
        void AddCollaboratorsAsync(ProjectCollaborator collaborator);
        ProjectCollaborator GetProjectCollaboratorsById(int projectId, string userId);
        void DeleteCollaboratorFromList(ProjectCollaborator projectCollaborator);
    }
}
