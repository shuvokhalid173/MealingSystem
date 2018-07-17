using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{

    public class Member_Bazar
    {
        public Member Member { get; set; }
        public decimal TotalBazar { get; set; }
    }

    public class Member_Meal
    {
        public Member Member { get; set; }
        public decimal TotalMeal { get; set; }
    }

    public class MealAccountInfo
    {
        public List<Member_Bazar> Member_Bazars { get; set; }
        public List<Member_Meal> Member_Meals { get; set; }
    }
}