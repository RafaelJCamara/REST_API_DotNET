using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ModelValidations
{
    public class Ticket_EnsureDueDateIsInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (ticket != null && ticket.TicketId==null)
            {
                if(ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now){
                    return new ValidationResult("Due date must be in the future!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
