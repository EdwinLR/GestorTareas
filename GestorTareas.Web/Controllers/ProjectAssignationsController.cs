using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;
using GestorTareas.Web.Helpers;
using GestorTareas.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestorTareas.Web.Controllers
{
    public class ProjectAssignationsController : Controller
    {
        private readonly ICombosHelper combosHelper;
        private readonly DataContext dataContext;
        private readonly IProjectRepository projectRepository;

        public ProjectAssignationsController(ICombosHelper combosHelper, DataContext dataContext, IProjectRepository projectRepository)
        {
            this.combosHelper = combosHelper;
            this.dataContext = dataContext;
            this.projectRepository = projectRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCollaborator(int id)
        {
            var model = new AddCollaboratorViewModel
            {
                UserId = -1,
                Users = combosHelper.GetComboUsers(),
                AssignedUsers = projectRepository.GetAllProjectCollaboratorsDetailTemps(),
                ProjectId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCollaborator(AddCollaboratorViewModel model)
        {
            if (this.ModelState.IsValid)
            {

                var user = await this.dataContext.Users.FindAsync(model.UserId);
                var project = await this.projectRepository.GetByIdAsync(model.ProjectId);

                if (user == null)
                {
                    return NotFound();
                }

                var projectCollaboratorsTemp = await this.dataContext.ProjectCollaboratorsDetailTemps.Where(odt => odt.User == user && odt.Project == project).FirstOrDefaultAsync();

                if (projectCollaboratorsTemp == null)
                {
                    projectCollaboratorsTemp = new ProjectCollaboratorsDetailTemp
                    {
                        Project = project,
                        User = user

                    };

                    //Repositorio
                    this.projectRepository.AddProjectCollaboratorDetailTemp(projectCollaboratorsTemp);
                }
                return RedirectToAction("AddCollaborators");
            }
            return View(model);
        }

        //Método Eliminar
        //public async Task<IActionResult> DeleteItem(int? id)
        //{
        //    if (id == null)
        //        return NotFound();

        //    var product = await this.dataContext.OrderDetailTemps.FindAsync(id);

        //    if (product == null)
        //        NotFound();

        //    this.dataContext.OrderDetailTemps.Remove(product);
        //    await this.dataContext.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        ////Método Confirm
        //public async Task<IActionResult> ConfirmOrder()
        //{
        //    var orderDetailTemps = await this.dataContext.OrderDetailTemps.Include(odt => odt.Product).ToListAsync();

        //    if (orderDetailTemps == null || orderDetailTemps.Count == 0)
        //        return NotFound();

        //    var details = orderDetailTemps.Select(odt => new OrderDetail
        //    {
        //        UnitPrice = odt.UnitPrice,
        //        Product = odt.Product,
        //        Quantity = odt.Quantity
        //    }).ToList();

        //    var order = new Order
        //    {
        //        OrderDate = DateTime.UtcNow,
        //        DeliveryDate = DateTime.UtcNow,
        //        Items = details,
        //        User = user
        //    };


        //    this.dataContext.Orders.Add(order);
        //    this.dataContext.OrderDetailTemps.RemoveRange(orderDetailTemps);

        //    await this.dataContext.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}
    }
}
