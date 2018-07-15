using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MealEventWithMembersViewModel
    {
        public string eventName { get; set; }

        public int eventMemberNumber { get; set; }

        public List<Member> memberList { get; set; }
    }
}