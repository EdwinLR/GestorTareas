using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class ProjectRepository : GenericRepository<Project>,
        IProjectRepository
    {
        private readonly DataContext context;
        public ProjectRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetProjectsWithConvocation()
        {
            return this.context.Projects.Include(c => c.Convocation);
        }

        public async Task<Project> GetProjectWithConvocationByIdAsync(int id)
        {
            return await this.context.Projects.Include(c => c.Convocation)
                .Include(p=>p.ProjectAssigments)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public void AddProjectCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaboratorsDetailTemp)
        {
            this.context.ProjectCollaboratorsDetailTemps.Add(projectCollaboratorsDetailTemp);
            this.context.SaveChangesAsync();
        }

        public IQueryable<ProjectCollaboratorsDetailTemp> GetAllProjectCollaboratorsDetailTemps()
        {
            return this.context.ProjectCollaboratorsDetailTemps
                .Include(idt => idt.User)
                .Include(i=>i.Project);
        }
    }
}
