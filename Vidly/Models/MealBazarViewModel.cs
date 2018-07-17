using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MealBazarViewModel
    {
        public DateTime Date { get; set; }
        public decimal Breakfast { get; set; }
        public decimal Launch { get; set; }
        public decimal Dinner { get; set; }
        public decimal Bazar { get; set; }
        public Member Member { get; set; }
    }
}