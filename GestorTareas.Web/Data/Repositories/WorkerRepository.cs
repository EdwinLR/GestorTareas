using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>,
        IWorkerRepository
    {
        private readonly DataContext context;

        public WorkerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWorkersWithUserAndPositionOrderByPosition()
        {
            return this.context.Workers
                        .Include(u => u.User)
                        .Include(p => p.Position)
                        .OrderBy(p => p.Position.Description);
        }

        public IQueryable GetAllWorkersWithUserAndPositionOrderByFatherLastname()
        {
            return this.context.Workers
                        .Include(u => u.User)
                        .Include(p => p.Position)
                        .OrderBy(u => u.User.FatherLastName);
        }

        public IQueryable<WorkerResponse> GetAllWorkersResponseWithUserAndPosition()
        {
            return this.context.Workers
                .Select(w => new WorkerResponse
                {
                    Id = w.Id,
                    FirstName = w.User.FirstName,
                    FatherLastName = w.User.FatherLastName,
                    MotherLastName = w.User.MotherLastName,
                    Email = w.User.Email,
                    PhoneNumber = w.User.PhoneNumber,
                    Position = w.Position.Description,
                    WorkerId = w.WorkerId,
                    UserId = w.User.Id
                });
        }

        public WorkerResponse GetWorkerResponseById(int id)
        {
            return this.context.Workers
                .Select(w => new WorkerResponse
                {
                    Id = w.Id,
                    FirstName = w.User.FirstName,
                    FatherLastName = w.User.FatherLastName,
                    MotherLastName = w.User.MotherLastName,
                    Email = w.User.Email,
                    PhoneNumber = w.User.PhoneNumber,
                    Position = w.Position.Description,
                    WorkerId = w.WorkerId,
                    UserId = w.User.Id
                }).FirstOrDefault(w => w.Id == id);
        }

        public Worker GetWorkerWithUserAndPositionById(int id)
        {
            return context.Workers
                .Include(u => u.User)
                .Include(p => p.Position)
                .FirstOrDefault(w => w.Id == id);
        }
    }
}

