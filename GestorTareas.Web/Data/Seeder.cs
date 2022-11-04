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
                await CheckCareer("Ingeniería de Software", "ISF");
                await CheckCareer("Ingeniería Mecatrónica", "IME");
            }

            if (!this.dataContext.Categories.Any())
            {
                await CheckCategory("Mantenimiento");
                await CheckCategory("Investigación");
                await CheckCategory("Limpieza");
                await CheckCategory("Orden");
                await CheckCategory("Trabajo en exterior");
            }

            if (!this.dataContext.ContactPeople.Any())
            {
                await CheckContactPerson("Gilberto", "Flores", "Reyes", "gilFR@ieee.com", 2224596532);
                await CheckContactPerson("Sandra", "Sanchez", "Serrano", "sanSS@cisco.com", 2498756229);
                await CheckContactPerson("Oscar", "Gonzalez", "Rodriguez", "oscGR@mexabat.com", 2481568765);
            }

            if (!this.dataContext.Countries.Any())
            {
                await CheckCountry("México");
                await CheckCountry("Estados Unidos");
                await CheckCountry("Canadá");
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

            if (!this.dataContext.Positions.Any())
            {
                await CheckPosition("Maestro Tiempo Completo");
                await CheckPosition("Maestro Medio Tiempo");
                await CheckPosition("Maestro Hora-Clase");
                await CheckPosition("Coordinador");
            }

            if (!this.dataContext.Institutes.Any())
            {
                var country = this.dataContext.Countries.FirstOrDefault(c => c.Id == 1);
                var contact = this.dataContext.ContactPeople.FirstOrDefault(c => c.Id == 1);
                await CheckInstitutes("Santander", "7564581597", "Independencia", "11A", "Centro Historico", "Monterrey", country, contact);
                contact = this.dataContext.ContactPeople.FirstOrDefault(c => c.Id == 2);
                await CheckInstitutes("IEEE", "5248157684", "Reforma", "456", "Venustiano Carranza", "Cuidad de México", country, contact);
                country = this.dataContext.Countries.FirstOrDefault(c => c.Id == 2);
                contact = this.dataContext.ContactPeople.FirstOrDefault(c => c.Id == 3);
                await CheckInstitutes("Microsoft", "6745981642", "Patriotism", "78G", "St. Patrick", "Seattle", country, contact);
            }

            if (!this.dataContext.Convocations.Any())
            {
                var institute = this.dataContext.Institutes.FirstOrDefault(c => c.Id == 1);
                await CheckConvocations(new DateTime(2022, 5, 1), new DateTime(2022, 11, 30), "Beca Santander Estudios", "Requisitos: ", "Premios: ", "https://app.becas-santander.com/es/program/becas-santander-estudios-apoyo-a-la-manutencion-2022", institute);
                institute = this.dataContext.Institutes.FirstOrDefault(c => c.Id == 2);
                await CheckConvocations(new DateTime(2022, 8, 1), new DateTime(2022, 9, 30), "World Championship 2022", "Requisitos: ", "Premios: ", "https://www.google.com.mx", institute);
                institute = this.dataContext.Institutes.FirstOrDefault(c => c.Id == 3);
                await CheckConvocations(new DateTime(2022, 10, 1), new DateTime(2023, 1, 27), "Imagine Cup 2022", "Requisitos: ", "Premios: ", "https://imaginecup.microsoft.com/es-es/Events", institute);
            }

            if (!this.dataContext.Students.Any())
            {
                var user = await CheckUser("Alejandro", "Barroeta", "Martinez", "alexbm@umad.edu.mx", "20071361ABM");
                var career = this.dataContext.Careers.FirstOrDefault(f => f.Id == 1);
                var gender = this.dataContext.Genders.FirstOrDefault(f => f.Id == 1);
                await CheckStudent(user, "Student", "20071361", career, gender);
            }

            if (!this.dataContext.Workers.Any())
            {
                var user = await CheckUser("Carlos", "Zapata", "Bretón", "carloszb@umad.edu.mx", "12345C");
                var gender = this.dataContext.Genders.FirstOrDefault(g => g.Id == 1);
                var position = this.dataContext.Positions.FirstOrDefault(p => p.Id == 4);
                await CheckWorker(user, "Coordinator", "1414", gender, position);

                user = await CheckUser("Edwin", "Lozada", "Ramos", "edwinlr@umad.edu.mx", "12345B");
                gender = this.dataContext.Genders.FirstOrDefault(g => g.Id == 1);
                position = this.dataContext.Positions.FirstOrDefault(p => p.Id == 1);
                await CheckWorker(user, "Teacher", "1417", gender, position);
            }

            if (!this.dataContext.Admins.Any())
            {
                var user = await CheckUser("Admin", "", "", "admin@umad.edu.mx", "12345A");
                await CheckAdmin(user, "Admin");
            }

        }

        private async Task CheckPosition(string description)
        {
            this.dataContext.Positions.Add(new Position
            {
                Description = description
            }
            );
            await this.dataContext.SaveChangesAsync();
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

        private async Task CheckCategory(string name)
        {
            this.dataContext.Categories.Add(new Category
            {
                CategoryName = name
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckContactPerson(string firstName, string fatherName, string motherName, string email, long phone)
        {
            this.dataContext.ContactPeople.Add(new ContactPerson
            {
                FirstName = firstName,
                FatherLastName = fatherName,
                MotherLastName = motherName,
                Email = email,
                PhoneNumber = phone
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckCountry(string name)
        {
            this.dataContext.Countries.Add(new Country
            {
                CountryName = name
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

        private async Task CheckInstitutes(string name, string contactPhone, string streetName, string streetNumber, string district, string city, Country country, ContactPerson contact)
        {
            this.dataContext.Institutes.Add(new Institute
            {
                Name = name,
                ContactPhone = contactPhone,
                StreetName = streetName,
                StreetNumber = streetNumber,
                District = district,
                City = city,
                Country = country,
                ContactPerson = contact
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckConvocations(DateTime startDate, DateTime endDate, string summary, string requirements, string prizes, string convocationURL, Institute institute)
        {
            this.dataContext.Convocations.Add(new Convocation
            {
                StartingDate = startDate,
                EndingDate = endDate,
                Summary = summary,
                Requirements = requirements,
                Prizes = prizes,
                ConvocationUrl = convocationURL,
                Institute = institute
            }
            );
            await this.dataContext.SaveChangesAsync();
        }

        private async Task CheckStudent(User user, string role, string studentId, Career career, Gender gender)
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

        private async Task CheckWorker(User user, string role, string workerId, Gender gender, Position position)
        {
            this.dataContext.Workers.Add(new Worker
            {
                User = user,
                WorkerId = workerId,
                Gender = gender,
                Position = position
            });
            await this.dataContext.SaveChangesAsync();
            await userHelper.AddUserToRoleAsync(user, role);
        }

        private async Task CheckAdmin(User user, string role)
        {
            this.dataContext.Admins.Add(new Admin
            {
                User = user
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
