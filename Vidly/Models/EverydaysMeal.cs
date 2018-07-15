using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class EverydaysMeal
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Breakfast { get; set; }

        [Required]
        public decimal Launch { get; set; }

        [Required]
        public decimal Dinner { get; set; }
        
        [Required]
        public Member Member { get; set; }
    }
}