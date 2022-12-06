using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
            return this.context.Projects
                .Include(c => c.Convocation)
                .Include(pc => pc.ProjectCollaborators)
                .ThenInclude(u => u.User);
        }

        public Project GetProjectWithConvocationAndCollaboratorsById(int id)
        {
            return this.context.Projects
                .Include(c => c.Convocation)
                .Include(p => p.ProjectCollaborators)
                .ThenInclude(u => u.User)
                .FirstOrDefault(p => p.Id == id);
        }

        //Responses Methods
        public IQueryable<ProjectResponse> GetAllProjectsResponse()
        {
            return this.context.Projects
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Convocation = p.Convocation.Summary,
                    ProjectCollaborators = p.ProjectCollaborators
                        .Select(pc => new ProjectColaboratorResponse
                        {
                            Id = pc.Id,
                            UserId = pc.User.Id
                        }).ToList()
                });
        }

        public Project GetProjectByName(string name)
        {
            return this.context.Projects.FirstOrDefault(p => p.ProjectName == name);
        }

        public ProjectResponse GetProjectResponseById(int id)
        {
            return this.context.Projects
                .Select(p => new ProjectResponse
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Convocation = p.Convocation.Summary,
                    ProjectCollaborators = p.ProjectCollaborators
                        .Select(pc => new ProjectColaboratorResponse
                        {
                            Id = pc.Id,
                            UserId = pc.User.Id
                        }).ToList()
                }).FirstOrDefault(c => c.Id == id);
        }

        //DetailTemps Methods
        public async Task<ProjectCollaboratorsDetailTemp> AddProjectCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaboratorsDetailTemp)
        {
            await this.context.ProjectCollaboratorsDetailTemps.AddAsync(projectCollaboratorsDetailTemp);
            await this.context.SaveChangesAsync();
            return projectCollaboratorsDetailTemp;
        }

        public IQueryable<ProjectCollaboratorsDetailTemp> GetAllProjectCollaboratorsDetailTemps()
        {
            return this.context.ProjectCollaboratorsDetailTemps
                .Include(idt => idt.User)
                .Include(i => i.Project);
        }

        public async Task<Project> AddCollaboratorsAsync(int id, List<ProjectCollaborator> collaborators, List<ProjectCollaboratorsDetailTemp> projectCollaboratorsTemp)
        {
            var project = this.context.Projects.FirstOrDefault(p => p.Id == id);
            project.ProjectCollaborators = collaborators;
            this.context.Projects.Update(project);
            this.context.ProjectCollaboratorsDetailTemps.RemoveRange(projectCollaboratorsTemp);
            await this.context.SaveChangesAsync();
            return project;
        }

        public ProjectCollaboratorsDetailTemp GetProjectCollaboratorsDetailTempsById(int id)
        {
            return this.context.ProjectCollaboratorsDetailTemps.Find(id);
        }

        public void DeleteCollaboratorDetailTemp(ProjectCollaboratorsDetailTemp projectCollaborator)
        {
            this.context.ProjectCollaboratorsDetailTemps.Remove(projectCollaborator);
            this.context.SaveChanges();
        }

        public ProjectCollaborator GetProjectCollaboratorsById(int projectId, string userId)
        {
           return this.context.ProjectCollaborators.FirstOrDefault(p => p.Project.Id == projectId && p.User.Id == userId );
        }

        public IQueryable<ProjectCollaborator> GetProjectCollaboratorsByProjectId(int projectId)
        {
            return this.context.ProjectCollaborators.Where(pc => pc.Project.Id == projectId);
        }

        public void DeleteCollaboratorFromList(ProjectCollaborator projectCollaborator)
        {
            this.context.ProjectCollaborators.Remove(projectCollaborator);
            this.context.SaveChanges();
        }

        public async void AddCollaboratorsAsync(ProjectCollaborator collaborator)
        {
            await this.context.ProjectCollaborators.AddAsync(collaborator);
            this.context.SaveChanges();
        }
    }
}
