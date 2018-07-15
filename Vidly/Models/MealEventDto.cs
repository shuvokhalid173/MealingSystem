using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MealEventDto
    {
        public MealEvent mealEvent { get; set; }

        public List<Member> members { get; set; }
    }
}