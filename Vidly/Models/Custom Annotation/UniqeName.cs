using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models.DTO;
using Vidly.Models;

namespace Vidly.Models.Custom_Annotation
{
    public class UniqeName :ValidationAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            IEnumerable<Customer> customers = db.Customers.ToList();

            foreach (Customer item in customers)
            {
                
                if  (item.Name.Equals(customer.Name) && customer.Id == 0) // it will work only for create new customer not for update
                {
                    return new ValidationResult("Customer name already exists");
                }
            }
            return ValidationResult.Success;
        }
    }

   
}