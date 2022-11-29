using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>,
        IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllStudentsWithUserAndCareerOrderByCareer()
        {
            return this.context
                        .Set<Student>()
                        .AsNoTracking()
                        .Include(u => u.User)
                        .Include(s => s.Career)
                        .OrderBy(s => s.Career.Name);
        }

        public IQueryable GetAllStudentsWithUserAndCareerOrderByFatherLastname()
        {
            return this.context
                        .Set<Student>()
                        .AsNoTracking()
                        .Include(u => u.User)
                        .Include(s => s.Career)
                        .OrderBy(u => u.User.FatherLastName);
        }

        public IQueryable GetAllStudentsWithUserAndCareerOrderByCareerId(int id)
        {
            return this.context
                        .Set<Student>()
                        .AsNoTracking()
                        .Include(u => u.User)
                        .Include(s => s.Career)
                        .Where(s => s.Career.Id == id)
                        .OrderBy(s => s.Career.Name);
        }

        public IQueryable<StudentResponse> GetAllStudentsResponseWithGenderAndCareer()
        {
            return this.context.Students
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    FirstName = s.User.FirstName,
                    FatherLastName = s.User.FatherLastName,
                    MotherLastName = s.User.MotherLastName,
                    Email = s.User.Email,
                    Gender = s.Gender.GenderName,
                    Career = s.Career.Name,
                    StudentId = s.StudentId,
                    UserId = s.User.Id,
                    AssignedActivities = s.AssignedActivities
                });
        }

        public StudentResponse GetStudentResponseById(int id)
        {
            return this.context.Students
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    FirstName = s.User.FirstName,
                    FatherLastName = s.User.FatherLastName,
                    MotherLastName = s.User.MotherLastName,
                    Email = s.User.Email,
                    Gender = s.Gender.GenderName,
                    Career = s.Career.Name,
                    StudentId = s.StudentId,
                    UserId = s.User.Id,
                    AssignedActivities = s.AssignedActivities
                }).FirstOrDefault(s => s.Id == id);
        }

        public Student GetStudentWithUserGenderAndCareerById(int id)
        {
            return context.Students
                .Include(u => u.User)
                .Include(s => s.Career)
                .Include(g => g.Gender)
                .Include(a => a.AssignedActivities)
                .FirstOrDefault(s => s.Id == id);
        }

        public async Task DeleteStudentAndUserAsync(Student student)
        {
            context.Students.Remove(student);

            var user = await context.Users
                .FindAsync(student.User.Id);
            context.Users.Remove(user);

            await context.SaveChangesAsync();
        }
    }
}
