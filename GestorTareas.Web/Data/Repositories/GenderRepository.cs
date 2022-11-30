using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Linq;

namespace GestorTareas.Web.Data.Repositories
{
    public class GenderRepository : GenericRepository<Gender>,
        IGenderRepository
    {
        private readonly DataContext context;

        public GenderRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Gender GetGenderById(int id)
        {
            return this.context.Genders.Find(id);
        }

        public IQueryable<GenderResponse> GetAllGendersResponsesWithStudents()
        {
            return this.context.Genders
                .Select(p => new GenderResponse
                {
                    Id = p.Id,
                    GenderName = p.GenderName,
                    Students = p.Students.Select(s => new StudentResponse
                    {
                        Id = s.Id,
                        StudentId = s.StudentId,
                        FirstName = s.User.FirstName,
                        FatherLastName = s.User.FatherLastName,
                        MotherLastName = s.User.MotherLastName,
                        Email = s.User.Email,
                        UserId = s.User.Id,
                        Career = s.Career.Name,
                        Gender = s.Gender.GenderName
                    })
                });
        }

        public GenderResponse GetGenderResponseWithStudentsById(int id)
        {
            return this.context.Genders
                .Select(p => new GenderResponse
                {
                    Id = p.Id,
                    GenderName = p.GenderName,
                    Students = p.Students.Select(s => new StudentResponse
                    {
                        Id = s.Id,
                        StudentId = s.StudentId,
                        FirstName = s.User.FirstName,
                        FatherLastName = s.User.FatherLastName,
                        MotherLastName = s.User.MotherLastName,
                        Email = s.User.Email,
                        UserId = s.User.Id,
                        Career = s.Career.Name,
                        Gender = s.Gender.GenderName
                    })
                }).FirstOrDefault(p => p.Id == id);
        }
    }
}