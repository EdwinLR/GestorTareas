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
                await CheckCareer("ISF");
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

        }

        private async Task CheckCareer(string name)
        {
            this.dataContext.Careers.Add(new Career
            {
                Name = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckGender(string name)
        {
            this.dataContext.Genders.Add(new Gender
            {
                Name = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckCategory(string name)
        {
            this.dataContext.Categories.Add(new Category
            {
                Name = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckPriority(string name)
        {
            this.dataContext.Priorities.Add(new Priority
            {
                Name = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckStatus(string name)
        {
            this.dataContext.Statuses.Add(new Status
            {
                Name = name
            }
            );
            await this.dataContext.SaveChangesAsync();
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
