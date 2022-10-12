using GestorTareas.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Web.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AssignedActivity> AssignedActivities { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<Convocation> Convocations { get; set; }
	public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCollaborator> ProjectCollaborators { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
