using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /*serves only to indicate the relationship with the Ticket table: Project has many Ticket*/
        public List<Ticket> Tickets { get; set; }
    }
}
