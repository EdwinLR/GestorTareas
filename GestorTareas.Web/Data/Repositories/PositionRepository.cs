using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class PositionRepository : GenericRepository<Position>,
        IPositionRepository
    {
        private readonly DataContext context;

        public PositionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Position> GetAllPositionsWithWorkers()
        {
            return this.context.Positions
                .Include(c => c.Workers)
                .ThenInclude(u => u.User);
        }

        public Position GetPositionWithWorkersById(int id)
        {
            return this.context.Positions
                .Include(c => c.Workers)
                .ThenInclude(u => u.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public Position GetPositionByName(string name)
        {
            return this.context.Positions.FirstOrDefault(p => p.Description == name);
        }

        public IQueryable<PositionResponse> GetAllPositionsResponsesWithWorkers()
        {
            return this.context.Positions
                .Select(p => new PositionResponse
                {
                    Id = p.Id,
                    Description = p.Description,
                    Workers = p.Workers.Select(w => new WorkerResponse
                    {
                        Id = w.Id,
                        WorkerId = w.WorkerId,
                        FirstName = w.User.FirstName,
                        FatherLastName = w.User.FatherLastName,
                        MotherLastName = w.User.MotherLastName,
                        Email = w.User.Email,
                        PhoneNumber = w.User.PhoneNumber,
                        Position = w.Position.Description,
                        UserId = w.User.Id
                    })
                });
        }

        public PositionResponse GetPositionResponseById(int id)
        {
            return this.context.Positions
               .Select(p => new PositionResponse
               {
                   Id = p.Id,
                   Description = p.Description,
                   Workers = p.Workers.Select(w => new WorkerResponse
                   {
                       Id = w.Id,
                       WorkerId = w.WorkerId,
                       FirstName = w.User.FirstName,
                       FatherLastName = w.User.FatherLastName,
                       MotherLastName = w.User.MotherLastName,
                       Email = w.User.Email,
                       PhoneNumber = w.User.PhoneNumber,
                       Position = w.Position.Description,
                       UserId = w.User.Id
                   })
               }).FirstOrDefault(p => p.Id == id);
        }
    }
}