using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>,
        IActivityRepository
    {
        private readonly DataContext context;

        public ActivityRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        //Activity Methods
        public IQueryable<Activity> GetAllActivitiesWithProjectsCategoriesPrioritiesStatuses()
        {
            return this.context.Activities
                .Include(a => a.Project)
                .Include(a => a.Category)
                .Include(a => a.Priority)
                .Include(a => a.Status)
                .Include(a => a.AssignedActivities)
                .ThenInclude(s => s.Career)
                .Include(a => a.AssignedActivities)
                .ThenInclude(s => s.User)
                .OrderBy(a => a.Title);
        }

        public async Task<Activity> GetActivityWithProjectCategoryPriorityStatusAsync(int id)
        {
            return await this.context.Activities
                .Include(a => a.Project)
                .Include(a => a.Category)
                .Include(a => a.Priority)
                .Include(a => a.Status)
                .Include(a => a.AssignedActivities)
                .ThenInclude(s => s.Career)
                .Include(a => a.AssignedActivities)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public IQueryable<ActivityResponse> GetAllActivitiesResponse()
        {
            return this.context.Activities
                .Select(a => new ActivityResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    Deadline = a.Deadline,
                    CreationDate = a.CreationDate,
                    Description = a.Description,
                    Progress = a.Progress,
                    Category = a.Category.CategoryName,
                    Status = a.Status.StatusName,
                    Priority = a.Priority.PriorityName,
                    Project = a.Project.ProjectName,
                    AssignedActivities = a.AssignedActivities
                        .Select(aa => new AssignedActivityResponse
                        {
                            Id = aa.Id,
                            StudentId = aa.StudentId
                        }).ToList()
                });
        }

        public ActivityResponse GetActivityResponseById(int id)
        {
            return this.context.Activities
                .Select(a => new ActivityResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    Deadline = a.Deadline,
                    CreationDate = a.CreationDate,
                    Description = a.Description,
                    Progress = a.Progress,
                    Category = a.Category.CategoryName,
                    Status = a.Status.StatusName,
                    Priority = a.Priority.PriorityName,
                    Project = a.Project.ProjectName,
                    AssignedActivities = a.AssignedActivities
                        .Select(aa => new AssignedActivityResponse
                        {
                            Id = aa.Id,
                            StudentId = aa.StudentId
                        }).ToList()
                }).FirstOrDefault(c => c.Id == id);
        }

        public async Task<Activity> CreateActivityAsync(Activity activity, List<ActivityDetailTemp> activitiesDetails)
        {
            await this.context.Activities.AddAsync(activity);
            this.context.ActivityDetailTemps.RemoveRange(activitiesDetails);
            await this.context.SaveChangesAsync();
            return activity;
        }


        //ActivityDetailTemp Methods
        public IQueryable<ActivityDetailTemp> GetAllActivityDetailTemps()
        {
            return this.context.ActivityDetailTemps
                .Include(adt => adt.Student);
        }
        public ActivityDetailTemp GetActivityDetailTempById(int id)
        {
            return this.context.ActivityDetailTemps.Find(id);
        }
        public ActivityDetailTemp GetActivityDetailTempByContactId(int id)
        {
            return this.context.ActivityDetailTemps.FirstOrDefault(adt => adt.Student.Id == id);
        }
        public void AddActivityDetailTemp(ActivityDetailTemp activityDetail)
        {
            this.context.ActivityDetailTemps.Add(activityDetail);
            this.context.SaveChanges();
        }
        public void DeleteActivityDetailTemp(ActivityDetailTemp activityDetail)
        {
            this.context.ActivityDetailTemps.Remove(activityDetail);
            this.context.SaveChanges();
        }
    }
}