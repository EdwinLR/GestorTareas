using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IProjectRepository: IGenericRepository<Project>
    {
        IQueryable GetProjectsWithConvocation();
        Task<Project> GetProjectWithConvocationByIdAsync(int id);
        void AddProjectCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaboratorsDetailTemp);
        IQueryable<ProjectCollaboratorsDetailTemp> GetAllProjectCollaboratorsDetailTemps();
    }
}
