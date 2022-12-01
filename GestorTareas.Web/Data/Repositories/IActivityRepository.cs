using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        //Metodos unicos de la entidad
        IQueryable<Activity> GetAllActivitiesWithProjectsCategoriesPrioritiesStatuses();
        Task<Activity> GetActivityWithProjectCategoryPriorityStatusAsync(int id);
        Task<Activity> CreateActivityAsync(Activity activity, List<ActivityDetailTemp> activitiesDetails);

        IQueryable<ActivityResponse> GetAllActivitiesResponse();
        ActivityResponse GetActivityResponseById(int id);

        IQueryable<ActivityDetailTemp> GetAllActivityDetailTemps();
        ActivityDetailTemp GetActivityDetailTempById(int id);
        ActivityDetailTemp GetActivityDetailTempByContactId(int id);
        void AddActivityDetailTemp(ActivityDetailTemp activityDetail);
        void DeleteActivityDetailTemp(ActivityDetailTemp activityDetail);
    }
}