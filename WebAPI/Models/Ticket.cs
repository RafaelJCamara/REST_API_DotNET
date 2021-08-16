using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ModelValidations;

namespace WebAPI.Models
{
    public class Ticket
    {

        public int? TicketId { get; set; }
        
        [Required]
        public int? ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }
        public DateTime? ReportDate { get; set; }

        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDueDateIsInFuture]
        public DateTime? DueDate { get; set; }

        /*serves only to indicate the relationship with the Project table: Ticket has one Project*/
        public Project Project { get; set; }
    }
}
