using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Web.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
