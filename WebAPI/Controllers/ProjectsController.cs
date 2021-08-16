using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public ProjectsController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Project.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            if (id <= 0)
            {
                return NotFound("ID must be greater than zero!");
            }

            var foundObject = _db.Project.Find(id);
            if (foundObject == null)
            {
                return NotFound("Project was not found!");
            }
            return Ok(foundObject);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] Project project)
        {
            _db.Project.Add(project);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.ProjectId) return BadRequest("IDs do not match!");
            _db.Entry(project).State = EntityState.Modified;
            
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                if (_db.Project.Find(id) == null)
                {
                    return NotFound("Project not found!");
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            if (id <= 0) return BadRequest("ID must be greater than zero!");
            var foundObject = _db.Project.Find(id);
            if (foundObject == null) return BadRequest("Project not found");
            _db.Project.Remove(foundObject);
            _db.SaveChanges();
            return Ok(foundObject);
        }


        /*
            api/projects (route atual)
            api/projects/{pid}/tickets?tid={tid}
         */

        [HttpGet("{pid}/tickets")]
        public IActionResult GetProjectTickets(int pId)
        {
            if (pId <= 0)
            {
                return NotFound("Process ID must be greater than zero!");
            }

            var tickets = _db.Ticket.Where(t=> t.ProjectId == pId).ToList();
            if(tickets == null || tickets.Count <= 0)
            {
                return NotFound();
            }
            return Ok(tickets);
        }


    }
}
