using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTareas.Web.Data;
using GestorTareas.Web.Data.Entities;
using GestorTareas.Web.Data.Repositories;

namespace GestorTareas.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository repository;

        public PrioritiesController(IPriorityRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetPriorities()
        {
            return Ok(this.repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPriority(int id)
        {
            var priority = this.repository.GetMasterById(id);

            if (priority == null)
            {
                return NotFound();
            }

            return Ok(priority);
        }
    }
}
