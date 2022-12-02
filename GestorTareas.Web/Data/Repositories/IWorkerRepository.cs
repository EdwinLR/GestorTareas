using GestorTareas.Common.Models;
using GestorTareas.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Data.Repositories
{
    public interface IWorkerRepository : IGenericRepository<Worker>
    {
        //Metodos unicos de la entidad
        IQueryable GetAllWorkersWithUserAndPositionOrderByPosition();
        IQueryable GetAllWorkersWithUserAndPositionOrderByFatherLastname();
        Worker GetWorkerWithUserAndPositionById(int id);
        Worker GetWorkerWithUserAndPositionByUserId(string userId);

        IQueryable<WorkerResponse> GetAllWorkersResponseWithUserAndPosition();
        WorkerResponse GetWorkerResponseById(int id);
    }
}