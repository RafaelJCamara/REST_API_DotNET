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
    public class TicketsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public TicketsController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult GetAllTickets()
        {
            return Ok(_db.Ticket.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetTicketById(int id)
        {
            if (id <= 0)
            {
                return NotFound("ID must be greater than zero!");
            }

            var foundObject = _db.Ticket.Find(id);
            if (foundObject == null)
            {
                return NotFound("Ticket not found!");
            }
            return Ok(foundObject);
        }

        [HttpPost]
        public IActionResult CreateTicket([FromBody] Ticket ticket)
        {
            _db.Ticket.Add(ticket);
            _db.SaveChanges();
            return 
                CreatedAtAction(nameof(GetTicketById), new { id = ticket.TicketId }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest("IDs do not match!");
            _db.Entry(ticket).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                if (_db.Ticket.Find(id) == null)
                {
                    return NotFound("Ticket not found!");
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            if (id < 0)
            {
                return BadRequest("ID must be greater than zero!");
            }

            var foundObject = _db.Ticket.Find(id);
            if (foundObject == null) return NotFound("Ticket not found!");
            _db.Ticket.Remove(foundObject);
            _db.SaveChanges();
            return Ok(foundObject);
        }

    }
}
