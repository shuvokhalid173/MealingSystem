using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MealEvent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60 , ErrorMessage = "Event Name should be maximum 60 character long")]
        public string EventName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int NumberOfMember { get; set; }
    }
}