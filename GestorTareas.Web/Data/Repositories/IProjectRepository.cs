using GestorTareas.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        IQueryable GetProjectsWithConvocation();
        Task<Project> GetProjectWithConvocationAndCollaboratorsByIdAsync(int id);
        Task<ProjectCollaboratorsDetailTemp> AddProjectCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaboratorsDetailTemp);
        IQueryable<ProjectCollaboratorsDetailTemp> GetAllProjectCollaboratorsDetailTemps();
        ProjectCollaboratorsDetailTemp GetProjectCollaboratorsDetailTempsById(int id);
        void DeleteCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaborator);
        Task<Project> AddCollaboratorsAsync(int id, List<ProjectCollaborator> collaborators, List<ProjectCollaboratorsDetailTemp> projectCollaboratorsTemp);
        ProjectCollaborator GetProjectCollaboratorsById(int projectId, string userId);
        void DeleteCollaboratorFromList(ProjectCollaborator projectCollaborator);
    }
}
