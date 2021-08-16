using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ModelValidations
{
 
    /*
        The main goal of this class is to make sure that, whenever a ticket has an owner, there is a due date attached to it
        To be used in the [] in the model validations
     */
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if(ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                //ticket exists and there is an owner
                if (!ticket.DueDate.HasValue)
                {
                    //there is no due date associated with an owned ticket
                    return new ValidationResult("Due date is required when a ticket has an owner!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
