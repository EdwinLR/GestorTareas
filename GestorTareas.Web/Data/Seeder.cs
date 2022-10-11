using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data
{
    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await userHelper.CheckRoleAsync("Admin");
            await userHelper.CheckRoleAsync("Coordinator");
            await userHelper.CheckRoleAsync("Student");
            await userHelper.CheckRoleAsync("Teacher");

            if (!this.dataContext.Careers.Any())
            {
                await CheckCareer("Ingeniería en Software", "ISF");
            }

            if (!this.dataContext.Categories.Any())
            {
                await CheckCategory("Mantenimiento");
                await CheckCategory("Investigación");
                await CheckCategory("Limpieza");
                await CheckCategory("Orden");
                await CheckCategory("Trabajo en exterior");
            }

            if (!this.dataContext.Genders.Any())
            {
                await CheckGender("Masculino");
                await CheckGender("Femenino");
                await CheckGender("No binario");
            }

            if (!this.dataContext.Priorities.Any())
            {
                await CheckPriority("Urgente");
                await CheckPriority("Normal");
                await CheckPriority("Puede esperar");
            }

            if (!this.dataContext.Statuses.Any())
            {
                await CheckStatus("Asignada");
                await CheckStatus("En proceso");
                await CheckStatus("Terminada");
            }

            if (!this.dataContext.Students.Any())
            {
                var user = await CheckUser("Alejandro", "Barroeta", "Martinez", "alexbm@umad.edu.mx", "20071361ABM");
                var career = this.dataContext.Careers.FirstOrDefault(f => f.Id == 1);
                var gender = this.dataContext.Genders.FirstOrDefault(f => f.Id == 1);
                await CheckStudent(user, "Student", 20071361, career, gender);
            }

        }

        private async Task CheckCareer(string name, string code)
        {
            this.dataContext.Careers.Add(new Career
            {
                Name = name,
                CareerCode = code
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckGender(string name)
        {
            this.dataContext.Genders.Add(new Gender
            {
                GenderName = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckCategory(string name)
        {
            this.dataContext.Categories.Add(new Category
            {
                CategoryName = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPriority(string name)
        {
            this.dataContext.Priorities.Add(new Priority
            {
                PriorityName = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckStatus(string name)
        {
            this.dataContext.Statuses.Add(new Status
            {
                StatusName = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckStudent(User user, string role, int studentId, Career career, Gender gender)
        {
            this.dataContext.Students.Add(new Student
            {
                User = user,
                StudentId = studentId,
                Career = career,
                Gender = gender
            });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, role);
        }

        private async Task<User> CheckUser(string firstName, string fatherlastName, string motherlastName, string email, string password)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    FatherLastName = fatherlastName,
                    MotherLastName = motherlastName,
                    Email = email,
                    UserName = email
                };
                var result = await userHelper.AddUserAsync(user, password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Error. No se pudo crear el usuario.");
                }
            }
            return user;
        }
    }
}
