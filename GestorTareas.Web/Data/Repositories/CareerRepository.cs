using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class CareerRepository : GenericRepository<Career>,
        ICareerRepository
    {
        private readonly DataContext context;

        public CareerRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Career> GetAllCareersWithStudents()
        {
            return this.context.Careers
                .Include(c => c.Students)
                .ThenInclude(c => c.Gender)
                .Include(c => c.Students)
                .ThenInclude(u => u.User);
        }

        public Career GetCareerById(int id)
        {
            return this.context.Careers
                .Include(s => s.Students)
                .ThenInclude(u => u.User)
                .FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<CareerResponse> GetAllCareersWithStudentsResponse()
        {
            //var students = this.context.Students.Where(s => s.)
            return this.context.Careers
                .Select(c => new CareerResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    CareerCode = c.CareerCode,
                    Students = c
                .Students
                .Select(s => new StudentResponse
                    {
                        Id = s.Id,
                        StudentId = s.StudentId,
                        Gender = s.Gender.GenderName
                    })
                });
        }

        public CareerResponse GetCareerByIdResponse(int id)
        {
            //return this.context.Careers
            //    .Include(s => s.Students)
            //    .ThenInclude(u => u.User)
            //    .FirstOrDefault(c => c.Id == id);
            return null;
        }
    }
}
